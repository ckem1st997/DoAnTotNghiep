using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Master.Filters;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Master.Extension;
using Master.Service;
using Master.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Master.SignalRHubs;

namespace Master.ConfigureServices.CustomConfiguration
{
    public static class CustomConfiguration
    {
        public static void AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(CustomValidationAttribute));
            })
                //   .AddApplicationPart(typeof(WareHousesController).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD.API", Version = "v1" });
            });
            services.AddDbContext<MasterdataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MasterdataContext"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );
            //  services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<IDapper, Dapperr>();
            services.AddScoped<IUserService, UserService>();
         //   services.AddScoped<IConnectRealTimeHub, ConnectRealTimeHub>();
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddMediatR(Assembly.GetExecutingAssembly());
            //   services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
        }

        //public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        //{
        //    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        //    var logstashUrl = configuration["Serilog:LogstashgUrl"];

        //    Log.Logger = new LoggerConfiguration()
        //        .MinimumLevel.Verbose()
        //        .Enrich.WithProperty("ApplicationContext", "Products")
        //        .Enrich.FromLogContext()
        //        .WriteTo.Console()
        //        //https://datalust.co/seq
        //        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl, apiKey: "vNdhiRHZqJJGWYAkYVlc")
        //        //  .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
        //        .ReadFrom.Configuration(configuration)
        //        .CreateLogger();

        //    return builder;
        //}
    }
}