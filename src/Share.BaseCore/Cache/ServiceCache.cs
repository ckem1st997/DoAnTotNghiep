using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IDatabase>(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(connect);
                return multiplexer.GetDatabase();
            });
            services.AddScoped<ICacheExtension, CacheExtension>();
            // nếu dùng Singleton thì sẽ run hàm khởi tạo một lần duy nhất cho đến khi service được khởi động lại
            // mà nếu như vậy thì lúc đầu connect thế nào thì suốt quá trình chạy service sẽ connect kiểu đó
            // nên là trong quá trình có sập redis hay không, không ảnh hưởng vì phụ thuộc vào lúc start service
            // là connect có redis hay không
            //  services.AddSingleton<ICacheExtension, CacheExtension>();

        }


        public static void AddEasyCachingAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEasyCaching(option =>
            {
                option.WithJson("myjson");

                // local
                option.UseInMemory("m1");
                // distributed
                option.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("127.0.0.1", 6379));
                    config.DBConfig.Database = 5;
                    config.SerializerName = "myjson";
                }, "myredis");

                // combine local and distributed
                option.UseHybrid(config =>
                {
                    config.TopicName = "test-topic";
                    config.EnableLogging = false;

                    // specify the local cache provider name after v0.5.4
                    config.LocalCacheProviderName = "m1";
                    // specify the distributed cache provider name after v0.5.4
                    config.DistributedCacheProviderName = "myredis";
                })
                // use redis bus
                .WithRedisBus(busConf =>
                {
                    busConf.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6380));

                // do not forget to set the SerializerName for the bus here !!
                busConf.SerializerName = "myjson";
                });
            });

        }
    }
}