using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Share.Base.Service.Caching;
using Share.Base.Service.Caching.CacheName;

namespace Share.Base.Service.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableMediatrQuery 
    {
        private readonly CacheSettings _settings;
        private readonly IHybridCachingProvider _easyCachingProvider;

        public CachingBehavior(IOptions<CacheSettings> settings,IHybridCachingProvider easyCachingProvider)
        {
            _settings = settings.Value;
            //    _configuration = configuration;
            //_cacheExtension = cacheExtension;
            //_cache = cache;
            _easyCachingProvider = easyCachingProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            if (request.BypassCache)
                return await next();
            // nếu data null thì chạy đến request tiếp theo để lấy data và gán vào cache
            var cachedResponse = await _easyCachingProvider.GetAsync<TResponse>(request.CacheKey, cancellationToken: cancellationToken);
            if (cachedResponse.HasValue)
            {
                response = cachedResponse.Value;
                Log.Information($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                Log.Information($"Added to Cache -> '{request.CacheKey}'.");
            }
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_settings.SlidingExpiration);
                var options = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = slidingExpiration
                };
                //   var serializedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await _easyCachingProvider.SetAsync<TResponse>(request.CacheKey, response, slidingExpiration, cancellationToken);
                return response;
            }
            return response;
        }
    }
}