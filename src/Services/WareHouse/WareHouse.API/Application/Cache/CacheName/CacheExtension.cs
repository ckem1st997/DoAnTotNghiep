using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using StackExchange.Redis;

namespace WareHouse.API.Application.Cache.CacheName
{
    public class CacheExtension : ICacheExtension
    {
        IConfiguration _configuration { get; set; }
        private readonly ILogger<CacheExtension> _logger;
        private readonly IDistributedCache _cache;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _db;
        public CacheExtension(IConfiguration configuration, IDistributedCache cache, ILogger<CacheExtension> logger)
        {
            _configuration = configuration;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionMultiplexer = GetConnectRedis();
            _db = _connectionMultiplexer != null ? _connectionMultiplexer.GetDatabase() : null;
        }

        public bool IsConnected
        {
            get { return _connectionMultiplexer != null && _connectionMultiplexer != null && _db != null && _connectionMultiplexer.IsConnected; }
        }



        public IEnumerable<string> GetAllNameKey()
        {
            List<string> listKeys = new List<string>();
            if (IsConnected)
            {
                var keys = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().FirstOrDefault()).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).ToList());
            }
            return listKeys;
        }


        /// <summary>
        /// connect to redis
        /// </summary>
        /// <returns></returns>
        private  ConnectionMultiplexer GetConnectRedis()
        {
            try
            {
                var getConnectString = _configuration.GetSection("Redis")["ConnectionString"];
                return ConnectionMultiplexer.Connect($"{getConnectString},allowAdmin=true");
            }
            catch (Exception ex)
            {
                Log.Error("Can not connect to Redis server !");
                Log.Error("Info: " + ex.Message);
                return null;
            }

        }

        public IEnumerable<string> GetAllNameKeyByContains(string contains)
        {
            if (contains == null)
                throw new ArgumentNullException(nameof(contains));
            if (GetAllNameKey() == null)
                return new List<string>();
            var list = GetAllNameKey().Where(x => x.Contains(contains));
            return list;
        }

        public async Task RemoveAllKeys()
        {
            var list = GetAllNameKey();
            if (list != null)

                foreach (var item in list)
                {
                    await _cache.RemoveAsync(item);
                }
        }

        public async Task RemoveAllKeysBy(string contains)
        {
            if (contains == null)
                throw new ArgumentNullException(nameof(contains));
            var list = GetAllNameKeyByContains(contains);
            if (list != null)
                foreach (var item in list)
                {
                    await _cache.RemoveAsync(item);
                }

        }

        public async Task<RedisResult> RemoveAll()
        {
            var res = await _db.ExecuteAsync("flushall");
            return res;
        }

        public void Dispose()
        {
        }
    }
}