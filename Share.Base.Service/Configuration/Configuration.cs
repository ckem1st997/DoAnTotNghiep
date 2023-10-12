

using KafKa.Net;
using Share.Base.Core.EventBus.Abstractions;
using Share.Base.Core.EventBus;
using Share.Base.Core.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Share.Base.Core.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Autofac.Core;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Share.Base.Core.Enum;
using Share.Base.Service.Repository;
using Share.Base.Core.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Share.Base.Service.Behaviors;
using Microsoft.AspNetCore.HttpOverrides;
using Share.Base.Service.Middleware;

namespace Share.Base.Service.Configuration
{
    public static class ConfigurationCore
    {
        // vào properties của dự án rồi bào build==> post build ==> điền xcopy /y "$(TargetDir)*.dll" ".\Core\"
        // "$(TargetDir)*.dll" : tất cả fill dll
        // ".\Core\" : thư mục lưu
        public static void UseBuiderAPI(this IApplicationBuilder app)
        {

            app.ConfigureSwagger();
            app.UseSerilogRequestLogging();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseMiddleware<RemoteIpAddressMiddleware>();
            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
        }
        public static void AddDataBaseContext<TDbContext>(this IServiceCollection services, IConfiguration configuration, string nameConnect, DatabaseType dbType = DatabaseType.MSSQL, QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll) where TDbContext : DbContext
        {
            var sqlConnect = configuration.GetConnectionString(nameConnect);
            services.AddDbContextPool<TDbContext>(options =>
            {
                options.UseSqlServer(sqlConnect,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TDbContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(
                        // Số lần tái thử tối đa
                        maxRetryCount: 15,
                        // Thời gian chờ tối đa giữa các lần tái thử
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        // Danh sách mã lỗi cụ thể để tái thử, null nếu muốn tái thử cho tất cả các lỗi 
                        errorNumbersToAdd: null
                        );
                    });
                options.LogTo(Log.Information, LogLevel.Information, Microsoft.EntityFrameworkCore.Diagnostics.DbContextLoggerOptions.UtcTime).EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(trackingBehavior);
            }
            );

            // Register dynamic dbContext
            //  services.AddScoped<DbContext, TDbContext>();

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

        public static void AddFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddOptions();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(FilterValidationAttribute));
            })
                // .AddApplicationPart(typeof(TStartup).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                }).AddNewtonsoftJson();
        }


        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }



        /// <summary>
        /// chú ý đọc doc của thư viện
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        public static void AddMediatRCore<T>(this IServiceCollection services) where T : class
        {
            // version 12.1
            services.AddMediatR(cfg =>
            {
                //  cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssembly(typeof(T).Assembly);
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            });
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        //kafka
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, EventKafKa>(
                sp =>
                {
                    var subscriptionClientName = configuration["SubscriptionClientName"];
                    var kafkaPersistentConnection = sp.GetRequiredService<IKafKaConnection>();
                    // var logger = sp.GetRequiredService<ILogger<EventKafKa>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventKafKa(configuration, kafkaPersistentConnection, eventBusSubcriptionsManager, subscriptionClientName);
                }
            );

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            // thêm xử lí event trong app
            //   services.AddTransient<TestIntegrationEventHandler>();
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        //public static void ConfigureEventBus(this IApplicationBuilder app)
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    // đăng ký xử lý
        //    eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
        //}
    }
}



