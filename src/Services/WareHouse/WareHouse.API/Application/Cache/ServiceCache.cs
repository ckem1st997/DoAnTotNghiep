using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WareHouse.API.Application.Cache.CacheName;

namespace WareHouse.API.Application.Cache
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
                ConnectTimeout = 3000
            };
            services.AddStackExchangeRedisCache(options =>
            {
                // options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                options.ConfigurationOptions = connect;
            });
            //Important step for Redis Caching       
            //services.AddEasyCaching(option =>
            //{
            //    option.UseRedis(config =>
            //    {
            //        config.DBConfig.Endpoints.Add(new ServerEndPoint(configuration.GetSection("Redis")["Server"], configuration.GetValue<int>("Redis:Port")));
            //    }, "rediswarehouse");
            //});
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.AddScoped<ICacheExtension, CacheExtension>();
            // nếu dùng Singleton thì sẽ run hàm khởi tạo một lần duy nhất cho đến khi service được khởi động lại
            // mà nếu như vậy thì lúc đầu connect thế nào thì suốt quá trình chạy service sẽ connect kiểu đó
            // nên là trong quá trình có sập redis hay không, không ảnh hưởng vì phụ thuộc vào lúc start service
            // là connect có redis hay không
            //  services.AddSingleton<ICacheExtension, CacheExtension>();

        }
    }
}