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
using WareHouse.Infrastructure;
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
using WareHouse.API.Application.SignalRService;
using WareHouse.API.Infrastructure.ElasticSearch;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using EasyCaching.Core;
using Aspose.Diagram;
using Share.BaseCore.Repositories;
using Share.BaseCore;
using Infrastructure;
using Share.BaseCore.Filters;
using Share.BaseCore.Behaviors;
using Microsoft.Extensions.Logging;

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
            services.AddDbContextPool<WarehouseManagementContext>(options =>
            {
                options.UseSqlServer(sqlConnect,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });

            }
            );
            // mulplite connect to dbcontext
            // services.AddDbContext<MasterdataContext>();
            // Register dynamic dbContext
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<DbContext, WarehouseManagementContext>();
            //  services.AddScoped<DbContext, MasterdataContext>();


            services.AddScoped(typeof(IElasticSearchClient<>), typeof(ElasticSearchClient<>));
            services.AddScoped<IDapper, Dapperr>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new Dapperr(config, "WarehouseManagementContext");

            });
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddScoped<IFakeData, FakeData>();
            services.AddScoped<IUserSevice, UserSevice>();
            services.AddScoped<ISignalRService, SignalRService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddScoped(typeof(GetServiceByInterface<>));


        }

        public static void ConfigureDBContext(this ContainerBuilder builder)
        {
           
        }
    }
}