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
        private readonly IHybridCachingManager _easyCachingProvider;

        public CachingBehavior(IOptions<CacheSettings> settings, IHybridCachingManager easyCachingProvider)
        {
            _settings = settings.Value;
            _easyCachingProvider = easyCachingProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            if (request.BypassCache || !_easyCachingProvider.IsConnectedRedis)
                return await next();
            var cachedResponse = await _easyCachingProvider.HybridCachingProvider.GetAsync<TResponse>(request.CacheKey, cancellationToken: cancellationToken);
            if (cachedResponse.HasValue)
            {
                response = cachedResponse.Value;
                Log.Information($"Get data from Cache -> '{request.CacheKey}'...");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                Log.Information($"Added data to Cache -> '{request.CacheKey}'...");
            }
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_settings.SlidingExpiration);
                await _easyCachingProvider.HybridCachingProvider.SetAsync<TResponse>(request.CacheKey, response, slidingExpiration, cancellationToken);
                return response;
            }
            return response;
        }
    }
}