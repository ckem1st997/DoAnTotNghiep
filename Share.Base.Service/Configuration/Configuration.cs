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
using Elastic.Apm.Api;
using Share.Base.Service.Caching;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Share.Base.Service.Security;
using System.Text;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Share.Base.Service.Validator;
using EFCoreSecondLevelCacheInterceptor;
using static Share.Base.Service.Caching.CacheName.CacheHelper;
using Microsoft.Extensions.Options;
using BaseHA.Core.Behaviors;
using Share.Base.Core.Grpc;
using GrpcAuthMaster;

namespace Share.Base.Service.Configuration
{
    public static class ConfigurationCore
    {
        // vào properties của dự án rồi bào build==> post build ==> điền xcopy /y "$(TargetDir)*.dll" ".\Core\"
        // "$(TargetDir)*.dll" : tất cả fill dll
        // ".\Core\" : thư mục lưu
        public static void UseAPI(this IApplicationBuilder app)
        {
            app.ConfigureSwagger();
            app.UseSerilogRequestLogging();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            //  app.UseMiddleware<RemoteIpAddressMiddleware>();
            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureRequestPipeline();
            // app.AppGraphQLServer();
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }
        /// <param name="application">Builder for configuring an application's request pipeline</param>


        //public static void ConfigureDBContext(this IServiceCollection application, WebApplicationBuilder web)
        //{
        //    web.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        //    web.Host.ConfigureContainer<ContainerBuilder>(builder =>
        //    {
        //        // Declare your services with proper lifetime

        //        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
        //        builder.RegisterType<BaseEngine>().As<IEngine>().SingleInstance();

        //        // truyền vào tên service và giá trịc cần truyền vào trong hàm khởi tạo
        //        builder.RegisterGeneric(typeof(TTT<>)).Named("Master", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => "master"))).InstancePerLifetimeScope();
        //        builder.RegisterGeneric(typeof(TTT<>)).Named("wh", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => "wh"))).InstancePerLifetimeScope();
        //        builder.RegisterGeneric(typeof(TTT<>)).As(typeof(ITTT<>)).InstancePerLifetimeScope();
        //    });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Program</typeparam>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddConfigureAPI<T>(this IServiceCollection services, IConfiguration Configuration) where T : class
        {
            services.AddControllers();
            services.AddEasyCachingCore(Configuration);
            services.AddFilter();
            services.AddSwagger();
            services.AddMediatR<T>();
            services.AddApiCors();
            services.AddApiElastic(Configuration);
            services.AddApiAuthentication();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddHttpContextAccessor();
            services.AddValidator();
            // grpc for auth
            services.AddApiGrpc<MasterAuth.MasterAuthClient>(Configuration.GetValue<string>("Grpc:Port"));
            //services.AddEFSecondLevelCache();
        }

        #region cache ef
        //AddEFSecondLevelCache
        public static void AddEFSecondLevelCache(this IServiceCollection services)
        {
            services.AddEFSecondLevelCache(options =>
        options.UseEasyCachingCoreProvider(CacheConfig.ProviderNames.Hybrid, isHybridCache: true)
        .DisableLogging(false)
        .UseCacheKeyPrefix("EF_")
          // .CacheAllQueriesExceptContainingTableNames(CacheExpirationMode.Absolute, TimeSpan.FromSeconds(CachingDefaults.DayCacheTime * CachingDefaults.CacheTime), realTableNames)
          .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromSeconds(CachingDefaults.DayCacheTime * CachingDefaults.CacheTime))
         .SkipCachingResults(result => result.Value == null || (result.Value is EFTableRows rows && rows.RowsCount == 0))
         );
        }


        #endregion


        #region Authozire
        public static void AddApiAuthentication(this IServiceCollection services)
        {

            //
            // services.AddScoped<IAuthorizeExtension, AuthorizeExtension>();

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
                            path.StartsWithSegments("/signalr"))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
        #endregion


        #region elk
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
        #endregion

        #region cors

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
        #endregion


        #region kafka
        public static void AddKafKaConnection(this IServiceCollection services, IConfiguration Configuration)
        {
            //  services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddEventBus(Configuration);
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

        #endregion


        #region database
        public static void AddDataBaseContext<TDbContext>(this IServiceCollection services, IConfiguration configuration, string nameConnect, DatabaseType dbType = DatabaseType.MSSQL, QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll) where TDbContext : DbContext
        {
            var sqlConnect = configuration.GetConnectionString(nameConnect);
            services.AddDbContextPool<TDbContext>(options =>
            {
                if (dbType == DatabaseType.MSSQL)
                    options.UseSqlServer(sqlConnect,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(TDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                            );
                        });
                else if (dbType == DatabaseType.Oracle)
                    options.UseOracle(sqlConnect,
                   oracleOptionsAction: sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(typeof(TDbContext).GetTypeInfo().Assembly.GetName().Name);
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

            //builder.RegisterType<TDbContext>().Named<DbContext>(ConnectionStringNames).InstancePerLifetimeScope();

        }

        #endregion





        #region extension
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
            });
        }


        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
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



        /// <summary>
        /// chú ý đọc doc của thư viện
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        public static void AddMediatR<T>(this IServiceCollection services) where T : class
        {
            // version 12.1
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(T).Assembly);
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                // cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });
        }

        #endregion

        //public static void ConfigureEventBus(this IApplicationBuilder app)
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    // đăng ký xử lý
        //    eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
        //}
    }
}



