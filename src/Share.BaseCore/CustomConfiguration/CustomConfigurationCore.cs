using Autofac;
using Autofac.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nest;
using Serilog;
using Share.BaseCore.Authozire;
using Share.BaseCore.BaseNop;
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
        public static void AddCustomConfigurationCore<TDbContext, TStartUp>(this IServiceCollection services, IConfiguration configuration, string nameConnect) where TDbContext : DbContext
        {
            var sqlConnect = configuration.GetConnectionString(nameConnect);
            services.AddDbContextPool<TDbContext>(options =>
            {
                options.UseSqlServer(sqlConnect,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TStartUp).GetTypeInfo().Assembly.GetName().Name);
                        // sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                options.LogTo(Log.Information, Microsoft.Extensions.Logging.LogLevel.Information, Microsoft.EntityFrameworkCore.Diagnostics.DbContextLoggerOptions.UtcTime).EnableSensitiveDataLogging();
                //  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            );

            // Register dynamic dbContext
            services.AddScoped<DbContext, TDbContext>();

        }

        public static void AddGeneric(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
        }

        public static void AddGeneric(this ContainerBuilder builder, string ConnectionStringNames, string ParameterName)
        {
            builder.RegisterGeneric(typeof(RepositoryEF<>))
       .Named(ConnectionStringNames, typeof(IRepositoryEF<>))
       .WithParameter(new ResolvedParameter(
           // kiểu truyền vào và tên biến truyền vào qua hàm khởi tạo
           (pi, ctx) => pi.ParameterType == typeof(DbContext) && pi.Name == ParameterName,
           (pi, ctx) => EngineContext.Current.Resolve<DbContext>(ConnectionStringNames))).InstancePerLifetimeScope();
        }

        /// <summary>
        /// Register resolving delegate DbContext
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="Lazy"></param>
        public static void AddRegisterDbContext(this ContainerBuilder builder, bool Lazy)
        {
            builder.Register<Func<string, DbContext>>(c =>
            {
                var cc = c.Resolve<IComponentContext>();
                return connectionStringName => cc.ResolveNamed<DbContext>(connectionStringName);
            });
            if (Lazy)
                builder.Register<Func<string, Lazy<DbContext>>>(c =>
                {
                    var cc = c.Resolve<IComponentContext>();
                    return connectionStringName => cc.ResolveNamed<Lazy<DbContext>>(connectionStringName);
                });
        }

        /// <summary>
        /// mulplite connect to dbcontext
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="builder"></param>
        /// <param name="ConnectionStringNames"></param>
        public static void AddDbContext<TDbContext>(this ContainerBuilder builder, string ConnectionStringNames) where TDbContext : DbContext
        {
            builder.RegisterType<TDbContext>().Named<DbContext>(ConnectionStringNames).InstancePerDependency();
        }


        public static void AddConfigurationCoreFilter<TStartup>(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddOptions();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(CustomValidationAttribute));
            })
                .AddApplicationPart(typeof(TStartup).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                }).AddNewtonsoftJson();
        }


        public static void AddSwaggerCore(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }





        /// <summary>
        /// Add generic repository services to the .NET Dependency Injection container.
        /// </summary>
        /// <typeparam name="TDbContext">Your EF Core <see cref="DbContext"/>.</typeparam>
        /// <param name="services">The type to be extended.</param>
        /// <param name="lifetime">The life time of the service.</param>
        /// <returns>Retruns <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
        //public static IServiceCollection AddGenericRepository<TDbContext>(
        //    this IServiceCollection services,
        //    ServiceLifetime lifetime = ServiceLifetime.Scoped)
        //    where TDbContext : DbContext
        //{
        //    if (services == null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    services.Add(new ServiceDescriptor(
        //        typeof(IGenericRepository),
        //        serviceProvider =>
        //        {
        //            TDbContext dbContext = ActivatorUtilities.CreateInstance<TDbContext>(serviceProvider);
        //            return new Repository<TDbContext>(dbContext);
        //        },
        //        lifetime));

        //    services.Add(new ServiceDescriptor(
        //       typeof(IGenericRepository<TDbContext>),
        //       serviceProvider =>
        //       {
        //           TDbContext dbContext = ActivatorUtilities.CreateInstance<TDbContext>(serviceProvider);
        //           return new Repository<TDbContext>(dbContext);
        //       },
        //       lifetime));

        //    return services;
        //}




        /// <summary>
        /// Add generic query repository services to the .NET Dependency Injection container.
        /// </summary>
        /// <typeparam name="TDbContext">Your EF Core <see cref="DbContext"/>.</typeparam>
        /// <param name="services">The type to be extended.</param>
        /// <param name="lifetime">The life time of the service.</param>
        /// <returns>Retruns <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
        //public static IServiceCollection AddQueryRepository<TDbContext>(
        //    this IServiceCollection services,
        //    ServiceLifetime lifetime = ServiceLifetime.Scoped)
        //    where TDbContext : DbContext
        //{
        //    if (services == null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    services.Add(new ServiceDescriptor(
        //        typeof(IQueryRepository),
        //        serviceProvider =>
        //        {
        //            TDbContext dbContext = ActivatorUtilities.CreateInstance<TDbContext>(serviceProvider);
        //            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //            return new QueryRepository<TDbContext>(dbContext);
        //        },
        //        lifetime));

        //    services.Add(new ServiceDescriptor(
        //        typeof(IQueryRepository<TDbContext>),
        //        serviceProvider =>
        //        {
        //            TDbContext dbContext = ActivatorUtilities.CreateInstance<TDbContext>(serviceProvider);
        //            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //            return new QueryRepository<TDbContext>(dbContext);
        //        },
        //        lifetime));

        //    return services;
        //}


    }
}



