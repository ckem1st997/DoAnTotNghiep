using EasyCaching.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Share.Base.Service.Caching.CacheName.CacheHelper;

namespace Share.Base.Service.Caching
{
    public interface IHybridCachingManager
    {
        IHybridCachingProvider HybridCachingProvider { get; }
        bool IsConnectedRedis { get; }

        /// <summary>
        /// Nếu Get Cache fail thì sẽ lấy dữ liệu từ Database (Local > Redis > Database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquirer"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        T GetDb<T>(string key, Func<T> acquirer, int? cacheTime = null);

        /// <summary>
        /// Nếu Get Cache fail thì sẽ lấy dữ liệu từ Database (Local > Redis > Database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquirer"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        Task<T> GetDbAsync<T>(string key, Func<Task<T>> acquirer, int? cacheTime = null);

    }

    public class HybridCachingManager : IHybridCachingManager
    {
        private readonly IDatabase _db;
        public IHybridCachingProvider HybridCachingProvider { get; }
        public HybridCachingManager(IHybridProviderFactory hybridFactory, IDatabase db)
        {
            HybridCachingProvider = hybridFactory.GetHybridCachingProvider(CacheConfig.ProviderNames.Hybrid);
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool IsConnectedRedis
        {
            get { return _db.Multiplexer.IsConnected; }
        }


        public virtual T GetDb<T>(string key, Func<T> acquirer, int? cacheTime = null)
        {
            var duration = cacheTime.HasValue && cacheTime.Value > 0
                ? TimeSpan.FromMinutes(cacheTime.Value)
                : TimeSpan.FromMinutes(CachingDefaults.CacheTime);
            var cacheValue = HybridCachingProvider.Get(key, acquirer, duration);
            if (cacheValue.HasValue)
                return cacheValue.Value;

            var result = acquirer();
            return result;
        }

        public virtual async Task<T> GetDbAsync<T>(string key, Func<Task<T>> acquirer, int? cacheTime = null)
        {
            var duration = cacheTime.HasValue && cacheTime.Value > 0
                ? TimeSpan.FromMinutes(cacheTime.Value)
                : TimeSpan.FromMinutes(CachingDefaults.CacheTime);
            var cacheValue = await HybridCachingProvider.GetAsync(key, acquirer, duration);
            if (cacheValue.HasValue)
                return cacheValue.Value;

            var result = await acquirer();
            return result;
        }
    }
}
