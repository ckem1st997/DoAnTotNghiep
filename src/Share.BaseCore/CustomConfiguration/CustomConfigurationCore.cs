using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Share.BaseCore.Authozire;
using Share.BaseCore.Extensions;
using Share.BaseCore.Filters;
using Share.BaseCore.IRepositories;
using Share.BaseCore.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.CustomConfiguration
{
    public static class CustomConfigurationCore
    {
        public static void AddCustomConfigurationCore<TControler, TDbContext, TStartUp>(this IServiceCollection services, IConfiguration configuration, string nameConnect) where TControler : class where TDbContext : DbContext
        {
            services.AddOptions();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(CustomValidationAttribute));
            })
                .AddApplicationPart(typeof(TControler).Assembly)
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = nameof(TControler), Version = "v1" });
            });
            var sqlConnect = configuration.GetConnectionString(nameConnect);
            Console.WriteLine("Set up Sql connect");
            Console.WriteLine(sqlConnect);
            services.AddDbContextPool<TDbContext>(options =>
            {
                options.UseSqlServer(sqlConnect,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TStartUp).GetTypeInfo().Assembly.GetName().Name);
                        // sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                options.LogTo(Log.Information, LogLevel.Information, Microsoft.EntityFrameworkCore.Diagnostics.DbContextLoggerOptions.UtcTime).EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            );
            // mulplite connect to dbcontext
            // services.AddDbContext<MasterdataContext>();
            // Register dynamic dbContext
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<DbContext, TDbContext>();
            //  services.AddScoped<DbContext, MasterdataContext>();

            services.AddScoped<IDapper, Dapperr>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new Dapperr(config, nameConnect);

            });
            services.AddScoped(typeof(GetServiceByInterface<>));


        }
    }
}



