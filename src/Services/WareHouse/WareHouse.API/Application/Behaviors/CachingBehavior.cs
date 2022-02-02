using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WareHouse.API.Application.Cache;

namespace WareHouse.API.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableMediatrQuery
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly CacheSettings _settings;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            // nếu không cache thì sẽ chạy đến request tiếp theo, ở đây là Handle Mediatr
            if (request.BypassCache)
                return await next();

            // nếu data null thì chạy đến request tiếp theo để lấy data và gán vào cache
            var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            if (cachedResponse is not null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(cachedResponse));
                _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
            }
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_settings.SlidingExpiration);
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