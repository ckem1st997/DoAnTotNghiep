using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Share.BaseCore.Cache;
using Share.BaseCore.Cache.CacheName;

namespace Share.BaseCore.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableMediatrQuery
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _settings;
        private readonly IConfiguration _configuration;
        private readonly ICacheExtension _cacheExtension;

        public CachingBehavior(ICacheExtension cacheExtension, IConfiguration configuration, IDistributedCache cache, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
            _configuration = configuration;
            _cacheExtension = cacheExtension;
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
            if (cachedResponse is not null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(cachedResponse));
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
                return response;
            }
            // cuối cùng trả về kết quả cho controller
            return response;
        }
    }
}