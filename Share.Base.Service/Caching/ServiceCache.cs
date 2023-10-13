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
using static Share.Base.Service.Caching.CacheName.CacheHelper;

namespace Share.Base.Service.Caching
{
    public static class ServiceCache
    {
        /// <summary>
        /// done HybridCachingProvider
        /// </summary>
        /// <param name="services"></param>
        public static void AddEasyCachingCore(this IServiceCollection services, IConfiguration configuration, EasyCachingType configEasyCaching = EasyCachingType.Hybrid)
        {
            services.AddSingleton(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configuration.GetSection("Redis")["ConnectionString"]);
                return multiplexer.GetDatabase();
            });
            //IHybridCachingManager
            // services.AddSingleton<IHybridCachingManager, HybridCachingManager>();
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

                if (configEasyCaching.Equals(EasyCachingType.InMemoryOnly) || configEasyCaching.Equals(EasyCachingType.Hybrid))
                    // local
                    option.UseInMemory(c =>
                    {
                        c.EnableLogging = true;
                    }, CacheHelper.CacheConfig.InMemory);
                if (configEasyCaching.Equals(EasyCachingType.RedisOnly) || configEasyCaching.Equals(EasyCachingType.Hybrid))
                    // distributed
                    option.UseRedis(config =>
                {
                    config.DBConfig.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                    //config.DBConfig.Database = 5;
                    config.DBConfig.AllowAdmin = true;
                    config.EnableLogging = true;
                    config.SerializerName = CacheHelper.CacheConfig.WithJson_Name;
                }, CacheHelper.CacheConfig.Redis).WithMessagePack(CacheHelper.CacheConfig.Redis);


                if (configEasyCaching.Equals(EasyCachingType.Hybrid))
                    // combine local and distributed
                    option.UseHybrid(config =>
                {
                    config.TopicName = CacheHelper.CacheConfig.TopicName;
                    config.EnableLogging = true;
                    // specify the local cache provider name after v0.5.4
                    config.LocalCacheProviderName = CacheHelper.CacheConfig.InMemory;
                    // specify the distributed cache provider name after v0.5.4
                    config.DistributedCacheProviderName = CacheHelper.CacheConfig.Redis;
                }, CacheConfig.ProviderNames.Hybrid)
                .WithRedisBus(busConf =>
                {
                    busConf.Configuration = configuration.GetSection("RedisBus")["ConnectionString"];
                    busConf.SerializerName = CacheHelper.CacheConfig.WithJson_Name;
                });
            });

        }

    }

}