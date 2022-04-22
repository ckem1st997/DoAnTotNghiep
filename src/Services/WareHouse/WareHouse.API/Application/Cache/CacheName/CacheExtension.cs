using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace WareHouse.API.Application.Cache.CacheName
{
    public class CacheExtension : ICacheExtension
    {
        IConfiguration _configuration { get; set; }
        private readonly IDistributedCache _cache;

        public CacheExtension(IConfiguration configuration, IDistributedCache cache)
        {
            _configuration = configuration;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public IEnumerable<string> GetAllNameKey()
        {
            List<string> listKeys = new List<string>();
            using ConnectionMultiplexer redis =
                ConnectionMultiplexer.Connect(
                    $"{_configuration.GetSection("Redis")["ConnectionString"]},allowAdmin=true");
            var keys = redis.GetServer("localhost", int.Parse(_configuration.GetSection("Redis")["Port"])).Keys();
            listKeys.AddRange(keys.Select(key => (string)key).ToList());
            return listKeys;
        }

        public IEnumerable<string> GetAllNameKeyByContains(string contains)
        {
            if (contains == null)
                throw new ArgumentNullException(nameof(contains));
            var list = GetAllNameKey().Where(x => x.Contains(contains));
            return list;
        }

        public async Task RemoveAllKeys()
        {
            var list = GetAllNameKey();
            foreach (var item in list)
            {
                await _cache.RemoveAsync(item);
            }
        }

        public async Task RemoveAllKeysBy(string contains)
        {
            if (contains == null)
                throw new ArgumentNullException(nameof(contains));
            if (_configuration.GetValue<bool>("UsingRedis") == true)
            {
                var list = GetAllNameKeyByContains(contains);
                foreach (var item in list)
                {
                    await _cache.RemoveAsync(item);
                }
            }

        }
    }
}