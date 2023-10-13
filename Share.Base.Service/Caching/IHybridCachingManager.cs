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
}
