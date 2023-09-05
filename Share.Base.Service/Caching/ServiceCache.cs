using EasyCaching.Core;
using EasyCaching.Core.Configurations;
using EasyCaching.Serialization.SystemTextJson.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Share.Base.Core.Enum;
using Share.Base.Service.Caching.CacheName;
using StackExchange.Redis;

namespace Share.Base.Service.Caching
{
    public static class ServiceCache
    {
        public static void AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDistributedMemoryCache();
            // Register the RedisCache service
            //services.AddMemoryCache();
            //var stringConnect = configuration.GetSection("Redis")["ConnectionString"];
            //Console.WriteLine(stringConnect);
            ////var connect = new ConfigurationOptions
            ////{
            ////   // Password = configuration.GetSection("Redis")["Password"],
            ////    EndPoints = { stringConnect },
            ////    AbortOnConnectFail = false,
            ////    ConnectTimeout = 1000
            ////};
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = stringConnect;
            //    options.InstanceName = CacheHelper.CacheConfig.Redis; // Tên của Redis instance (nếu cần)
            //});
            //Configure other services up here
            //services.AddSingleton(cfg =>
            //{
            //    IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(stringConnect);
            //    return multiplexer.GetDatabase();
            //});
           // services.AddSingleton<ICacheExtension, CacheExtension>();
            // nếu dùng Singleton thì sẽ run hàm khởi tạo một lần duy nhất cho đến khi service được khởi động lại
            // mà nếu như vậy thì lúc đầu connect thế nào thì suốt quá trình chạy service sẽ connect kiểu đó
            // nên là trong quá trình có sập redis hay không, không ảnh hưởng vì phụ thuộc vào lúc start service
            // là connect có redis hay không
            //  services.AddSingleton<ICacheExtension, CacheExtension>();

        }




        /// <summary>
        /// done HybridCachingProvider
        /// </summary>
        /// <param name="services"></param>
        public static void AddEasyCachingCore(this IServiceCollection services, IConfiguration configuration, ConfigEasyCaching configEasyCaching = ConfigEasyCaching.Hybrid)
        {
            services.AddSingleton<IHybridProviderFactory, DefaultHybridProviderFactory>();
            //IHybridCachingManager
            services.AddSingleton<IHybridCachingManager, HybridCachingManager>();
            services.AddEasyCaching(option =>
            {
                Action<JsonSerializerSettings> serializerSettings = x =>
                {
                    x.MissingMemberHandling = MissingMemberHandling.Ignore;
                    x.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                    x.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    x.MaxDepth = 32;
                };
                option.WithJson(serializerSettings, CacheHelper.CacheConfig.WithJson_Name);

                if (configEasyCaching.Equals(ConfigEasyCaching.InMemoryOnly) || configEasyCaching.Equals(ConfigEasyCaching.Hybrid))
                    // local
                    option.UseInMemory(CacheHelper.CacheConfig.InMemory);
                if (configEasyCaching.Equals(ConfigEasyCaching.RedisOnly) || configEasyCaching.Equals(ConfigEasyCaching.Hybrid))
                    // distributed
                    option.UseRedis(config =>
                {
                    config.DBConfig.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                    //config.DBConfig.Database = 5;
                    config.DBConfig.AllowAdmin = true;
                    config.SerializerName = CacheHelper.CacheConfig.WithJson_Name;
                }, CacheHelper.CacheConfig.Redis).WithMessagePack(CacheHelper.CacheConfig.Redis);


                if (configEasyCaching.Equals(ConfigEasyCaching.Hybrid))
                    // combine local and distributed
                    option.UseHybrid(config =>
                {
                    config.TopicName = CacheHelper.CacheConfig.TopicName;
                    config.EnableLogging = true;

                    // specify the local cache provider name after v0.5.4
                    config.LocalCacheProviderName = CacheHelper.CacheConfig.InMemory;
                    // specify the distributed cache provider name after v0.5.4
                    config.DistributedCacheProviderName = CacheHelper.CacheConfig.Redis;
                })
                // use redis bus
                .WithRedisBus(busConf =>
                {
                    busConf.Configuration= configuration.GetSection("RedisBus")["ConnectionString"];
                    // do not forget to set the SerializerName for the bus here !!
                    busConf.SerializerName = CacheHelper.CacheConfig.WithJson_Name;
                });
            });

        }

    }

}