using System;

namespace WareHouse.API.Application.Cache
{
    public interface ICacheableMediatrQuery
    {
        /// <summary>
        /// false:get cache
        /// true: no get cache
        /// </summary>
        bool BypassCache { get; }
        /// <summary>
        /// Cache Key
        /// </summary>
        string CacheKey { get; }
        /// <summary>
        /// time exits
        /// </summary>
        TimeSpan? SlidingExpiration { get; }
    }
}