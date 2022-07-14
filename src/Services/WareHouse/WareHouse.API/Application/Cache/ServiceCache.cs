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
                ConnectTimeout=3000
            };
            services.AddStackExchangeRedisCache(options =>
            {
                // options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                options.ConfigurationOptions = connect;
            });
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.AddScoped<ICacheExtension, CacheExtension>();
        }
    }
}