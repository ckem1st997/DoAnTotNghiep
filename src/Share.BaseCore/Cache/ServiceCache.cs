using EasyCaching.Core.Configurations;
using EasyCaching.Serialization.SystemTextJson.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Share.BaseCore.Cache.CacheName;
using StackExchange.Redis;

namespace Share.BaseCore.Cache
{
    public static class ServiceCache
    {
        public static void AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedMemoryCache();
            // Register the RedisCache service
            //  services.AddMemoryCache();
            var stringConnect = configuration.GetSection("Redis")["ConnectionString"];
            Console.WriteLine(stringConnect);
            var connect = new ConfigurationOptions
            {
                Password = configuration.GetSection("Redis")["Password"],
                EndPoints = { stringConnect },
                AbortOnConnectFail = false,
                ConnectTimeout = 1000
            };
            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = connect;
            });
            //Configure other services up here
            services.AddSingleton<IDatabase>(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(connect);
                return multiplexer.GetDatabase();
            });
            services.AddSingleton<ICacheExtension, CacheExtension>();
            // nếu dùng Singleton thì sẽ run hàm khởi tạo một lần duy nhất cho đến khi service được khởi động lại
            // mà nếu như vậy thì lúc đầu connect thế nào thì suốt quá trình chạy service sẽ connect kiểu đó
            // nên là trong quá trình có sập redis hay không, không ảnh hưởng vì phụ thuộc vào lúc start service
            // là connect có redis hay không
            //  services.AddSingleton<ICacheExtension, CacheExtension>();

        }


        public static void AddEasyCachingAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var stringConnect = configuration.GetSection("Redis")["ConnectionString"];

            services.AddEasyCaching(option =>
            {
                option.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint(configuration.GetSection("Redis")["Server"], int.Parse(configuration.GetSection("Redis")["Port"])));
                    config.EnableLogging = true;
                    config.SerializerName = "redis";
                }, "redis")
                .WithMessagePack("redis");
                Action<JsonSerializerSettings> serializerSettings = x =>
                {
                    x.MissingMemberHandling = MissingMemberHandling.Ignore;
                    x.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                    x.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    x.MaxDepth = 32;
                };
                option.WithJson(serializerSettings, "json");
            });


            //services.AddEasyCaching(delegate (EasyCachingOptions options)
            //{
            //    options.UseInMemory(configuration, CachingHelper.Configs.ProviderNames.InMemory, CachingHelper.Configs.ConfigSectionNames.InMemory);
            //    options.UseRedis(configuration, CachingHelper.Configs.ProviderNames.Redis, CachingHelper.Configs.ConfigSectionNames.Redis);
            //    options.UseHybrid(delegate (HybridCachingOptions config)
            //    {
            //        config.TopicName = CachingHelper.Configs.HybridTopicName;
            //        config.EnableLogging = true;
            //        config.LocalCacheProviderName = CachingHelper.Configs.ProviderNames.InMemory;
            //        config.DistributedCacheProviderName = CachingHelper.Configs.ProviderNames.Redis;
            //    }, CachingHelper.Configs.ProviderNames.Hybrid).WithRedisBus(configuration, CachingHelper.Configs.ConfigSectionNames.RedisBus);
            //    Action<JsonSerializerSettings> jsonSerializerSettingsConfigure = delegate (JsonSerializerSettings x)
            //    {
            //        x.MissingMemberHandling = MissingMemberHandling.Ignore;
            //        x.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //        x.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
            //        x.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            //        x.MaxDepth = 32;
            //    };
            //    options.WithJson(jsonSerializerSettingsConfigure, CachingHelper.SerializerNames.Json);
            //});

        }
    }
}