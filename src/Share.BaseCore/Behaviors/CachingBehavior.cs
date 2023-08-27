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
using Share.BaseCore.Cache;
using Share.BaseCore.Cache.CacheName;

namespace Share.Base.Service.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableMediatrQuery
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _settings;
        // private readonly IConfiguration _configuration;
        private readonly ICacheExtension _cacheExtension;
        //  private readonly IEasyCachingProvider _easyCachingProvider;

        public CachingBehavior(ICacheExtension cacheExtension, IOptions<CacheSettings> settings, IDistributedCache cache)
        {
            _settings = settings.Value;
            //    _configuration = configuration;
            _cacheExtension = cacheExtension;
            // _easyCachingProvider = easyCachingProvider;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            // nếu không cache thì sẽ chạy đến request tiếp theo, ở đây là Handle Mediatr
            // nếu không connect được thì lấy data ở dưới db

            if (request.BypassCache)
                return await next();
            if (!_cacheExtension.IsConnected)
            {
                Log.Information("Redis not connected !");
                return await next();
            }
            // nếu data null thì chạy đến request tiếp theo để lấy data và gán vào cache
            var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            //   var cachedResponse = await _easyCachingProvider.GetAsync<TResponse>(request.CacheKey, cancellationToken: cancellationToken);
            //  if (cachedResponse.HasValue)
            if (cachedResponse is not null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(cachedResponse));
                // response = cachedResponse.Value;
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
                var serializedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
                //  await _easyCachingProvider.SetAsync<TResponse>(request.CacheKey, response, slidingExpiration, cancellationToken);
                return response;
            }
            // cuối cùng trả về kết quả cho controller
            return response;
        }
    }
}