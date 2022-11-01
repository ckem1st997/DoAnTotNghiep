﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Controllers;
using WareHouse.API.Filters;
using WareHouse.Domain.IRepositories;
using WareHouse.Infrastructure;
using WareHouse.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Authentication;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.SignalRService;
using WareHouse.API.Infrastructure.ElasticSearch;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Core;
using EasyCaching.Core;
using Nest;
using Aspose.Diagram;
using Core.Extensions;

namespace WareHouse.API.ConfigureServices.CustomConfiguration
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
                // options.Filters.Add(typeof(CheckRoleAttribute));
            })
                .AddApplicationPart(typeof(WareHousesController).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                }).AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WH.API", Version = "v1" });
            });
            var sqlConnect = configuration.GetConnectionString("WarehouseManagementContext");
            services.AddDbContext<WarehouseManagementContext>(options =>
            {
                options.UseSqlServer(sqlConnect,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });

            },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            // Register dynamic dbContext
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<DbContext, WarehouseManagementContext>();


            services.AddScoped(typeof(IElasticSearchClient<>), typeof(ElasticSearchClient<>));
            services.AddScoped<IDapper, Dapperr>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new Dapperr(config);

            });
            services.AddScoped<IFakeData, FakeData>();
            services.AddScoped<IUserSevice, UserSevice>();
            services.AddScoped<ISignalRService, SignalRService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddScoped(typeof(GetServiceByInterface<>));


        }

        public static void ConfigureDBContext(this IServiceCollection application, WebApplicationBuilder web)
        {
            web.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            web.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                // Declare your services with proper lifetime

                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
                builder.RegisterType<BaseEngine>().As<IEngine>().SingleInstance();
              //  //  builder.RegisterType<WarehouseManagementContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
              //  // truyền vào tên service và giá trịc cần truyền vào trong hàm khởi tạo
              //  //builder.RegisterGeneric(typeof(RepositoryEF<>)).Named(DataConnectionHelper.ConnectionStringNames.Warehouse, typeof(IRepositoryEF<>)).
              //  //WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>
              //  //(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)
              //  //((pi, ctx) => pi.ParameterType == typeof(DbContext) && pi.Name == DataConnectionHelper.ConnectionStringNames.Warehouse), (Func<ParameterInfo, IComponentContext, object>)
              //  //((pi, ctx) => EngineContext.Current.Resolve<WarehouseManagementContext>()))).InstancePerLifetimeScope();
              //  //  builder.RegisterGeneric(typeof(RepositoryEF<>)).As(typeof(IRepositoryEF<>)).InstancePerLifetimeScope();
              //  //
              //  builder.RegisterGeneric(typeof(RepositoryEF<>)).Named(DataConnectionHelper.ConnectionStringNames.Warehouse, typeof(IRepositoryEF<>))
              //  .WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>
              //  (new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)(
              //  (pi, ctx) => pi.ParameterType == typeof(WarehouseManagementContext) && pi.Name == "key"), 
              //  (Func<ParameterInfo, IComponentContext, object>)
              //  ((pi, ctx) => EngineContext.Current.Resolve<WarehouseManagementContext>()))).InstancePerLifetimeScope();
              ////  builder.RegisterGeneric(typeof(RepositoryEF<>)).As(typeof(IRepositoryEF<>)).InstancePerLifetimeScope();
            });
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