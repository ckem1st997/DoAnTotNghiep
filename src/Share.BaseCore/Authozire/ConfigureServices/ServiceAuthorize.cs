using AutoMapper.Configuration;
using Elasticsearch.Net;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Serilog;
using Share.BaseCore.Extensions;
using Share.BaseCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Share.BaseCore.Authozire.ConfigureServices
{
    public static class ServiceAuthorize
    {
        public static void InitAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            CommonHelper.BaseConfig = configuration.GetSection("JWT").Get<BaseSsoConfig>();
        }
        public static void AddApiAuthentication(this IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            //
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthozireExtensionForMaster, AuthozireExtensionForMaster>();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = AuthozireStringHelper.JWT.ValidAudience,
                    ValidIssuer = AuthozireStringHelper.JWT.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthozireStringHelper.JWT.Secret))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/signalr")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
        public static void AddApiCors(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));
        }     
        
        public static void AddApiElastic(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<IElasticClient, ElasticClient>(sp =>
            {
                var connectionPool = new SingleNodeConnectionPool(new Uri(Configuration.GetValue<string>("Elastic:Url")));
                var settings = new ConnectionSettings(connectionPool, (builtInSerializer, connectionSettings) =>
                    new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

                    })).DefaultIndex(Configuration.GetValue<string>("Elastic:Index")).DisableDirectStreaming()
                    .PrettyJson()
                    .RequestTimeout(TimeSpan.FromSeconds(2))
                    .OnRequestCompleted(apiCallDetails =>
                    {
                        var list = new List<string>();
                        // log out the request and the request body, if one exists for the type of request
                        if (apiCallDetails.RequestBodyInBytes != null)
                        {
                            Log.Information(
                                $"{apiCallDetails.HttpMethod} {apiCallDetails.Uri} " +
                                $"{Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes)}");
                        }
                        else
                        {
                            Log.Information($"{apiCallDetails.HttpMethod} {apiCallDetails.Uri}");
                        }

                        // log out the response and the response body, if one exists for the type of response
                        if (apiCallDetails.ResponseBodyInBytes != null)
                        {
                            Log.Information($"Status: {apiCallDetails.HttpStatusCode}" +
                                        $"{Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes)}");
                        }
                        else
                        {
                            Log.Information($"Status: {apiCallDetails.HttpStatusCode}");
                        }
                    });
                return new ElasticClient(settings);
            });

        }


        public static void AddApiLogging(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddLogging(loggingBuilder =>
            //{
            //    var seqServerUrl = configuration["Serilog:SeqServerUrl"];

            //    loggingBuilder.AddSeq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl,
            //        apiKey: "0QEfAbE4THZTcUu6I7bQ");
            //});
        }

    }
}