using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.AutoMapper.ConfigureServices;
using WareHouse.API.Application.Behaviors.ConfigureServices;
using WareHouse.API.Application.Cache;
using WareHouse.API.Application.Validations.ConfigureServices;
using WareHouse.API.ConfigureServices.CustomConfiguration;
using Grpc.Net.Client.Web;
using GrpcGetDataToMaster;
using System.Net;
using System.Net.Http;
using System.Reflection;
using WareHouse.API.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WareHouse.API.Application.Authentication;
using Grpc.Net.ClientFactory;
using GrpcGetDataToWareHouse;
using WareHouse.API.IntegrationEvents;
using KafKa.Net;
using WareHouse.API.Application.Extensions;
using Nest;
using Elasticsearch.Net;
using Serilog;
using static Nest.ConnectionSettings;
using Newtonsoft.Json;
using Nest.JsonNetSerializer;

namespace WareHouse.API
{
    //Scaffold-DbContext "Server=tcp:127.0.0.1,5433;Initial Catalog=WarehouseManagement;Persist Security Info=True;User ID=sa;Password=Aa!0977751021;MultipleActiveResultSets = true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetLicense();
            services.AddCustomConfiguration(Configuration);
            services.AddMapper();
            services.AddValidator();
            services.AddBehavior();
            services.AddCache(Configuration);
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

            services.AddLogging(loggingBuilder =>
            {
                //   loggingBuilder.UseSerilog(Configuration);
                var seqServerUrl = Configuration["Serilog:SeqServerUrl"];

                loggingBuilder.AddSeq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl,
                    apiKey: "0QEfAbE4THZTcUu6I7bQ");
            });

            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddGrpc(options => { options.EnableDetailedErrors = true; });
            services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddEventBus(Configuration);

            services.AddScoped<IElasticClient, ElasticClient>(sp =>
            {
                var connectionPool = new SingleNodeConnectionPool(new Uri(Configuration.GetValue<string>("Elastic:Url")));
                var settings = new ConnectionSettings(connectionPool, (builtInSerializer, connectionSettings) =>
                    new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })).DefaultIndex(Configuration.GetValue<string>("Elastic:Index")).DisableDirectStreaming()
                    .PrettyJson()
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
                //var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection())
                //.DefaultIndex("mssql-warehouse")
                //.DisableDirectStreaming()
                //.OnRequestCompleted(apiCallDetails =>
                //{
                //    var list = new List<string>();
                //    // log out the request and the request body, if one exists for the type of request
                //    if (apiCallDetails.RequestBodyInBytes != null)
                //    {
                //        Log.Information(
                //            $"{apiCallDetails.HttpMethod} {apiCallDetails.Uri} " +
                //            $"{Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes)}");
                //    }
                //    else
                //    {
                //        Log.Information($"{apiCallDetails.HttpMethod} {apiCallDetails.Uri}");
                //    }

                //    // log out the response and the response body, if one exists for the type of response
                //    if (apiCallDetails.ResponseBodyInBytes != null)
                //    {
                //        Log.Information($"Status: {apiCallDetails.HttpStatusCode}" +
                //                    $"{Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes)}");
                //    }
                //    else
                //    {
                //        Log.Information($"Status: {apiCallDetails.HttpStatusCode}");
                //    }
                //});
                return new ElasticClient(settings);
            });


            AppContext.SetSwitch(
  "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            services.AddGrpcClient<GrpcGetData.GrpcGetDataClient>(o =>
                {
                    o.Address = new Uri(Configuration.GetValue<string>("Grpc:Port"));
                }).AddInterceptor<GrpcExceptionInterceptor>(InterceptorScope.Client)
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(httpHandler));
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
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });
            services.AddHttpContextAccessor();
            // services.AddSingleton<IHostedService, RequestTimeConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WareHouse.API v1"));
            //  }

            //   app.UseHttpsRedirection();

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcGetDataWareHouseService>().EnableGrpcWeb();
                endpoints.MapControllers();
            });
            app.ConfigureEventBus();
        }


        private static void SetLicense()
        {
            new Aspose.BarCode.License().SetLicense(AsposeHelper.BarCodeLicenseStream);
            new Aspose.Cells.License().SetLicense(AsposeHelper.CellsLicenseStream);
            new Aspose.Pdf.License().SetLicense(AsposeHelper.PdfLicenseStream);
            new Aspose.Words.License().SetLicense(AsposeHelper.WordsLicenseStream);
            // Fix tương thích trên .NET Core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}