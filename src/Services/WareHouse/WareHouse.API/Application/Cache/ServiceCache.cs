using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace WareHouse.API.Application.Cache
{
    public static class ServiceCache
    {

        public static void AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedMemoryCache();
            // Register the RedisCache service
            //  services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                // options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Password = configuration.GetSection("Redis")["Password"],
                    EndPoints = { configuration.GetSection("Redis")["ConnectionString"] }
                };
            });
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            //services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
        }
    }
}