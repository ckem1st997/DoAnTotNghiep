using EasyCaching.Core;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Share.Base.Service.Caching.CacheName.CacheHelper;

namespace Share.Base.Service.Caching
{
    [SingletonDependency]
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
                : TimeSpan.FromMinutes(CachingDefaults.DayCacheTime);
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
                : TimeSpan.FromMinutes(CachingDefaults.DayCacheTime);
            var cacheValue = await HybridCachingProvider.GetAsync(key, acquirer, duration);
            if (cacheValue.HasValue)
                return cacheValue.Value;

            var result = await acquirer();
            return result;
        }

        public virtual async Task<T> GetDbAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));
            }

            var cacheValue = await HybridCachingProvider.GetAsync<T>(key);
            return cacheValue.Value;
        }

        public virtual T GetDb<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));
            }

            var cacheValue = HybridCachingProvider.Get<T>(key);
            return cacheValue.Value;
        }
    }
}
