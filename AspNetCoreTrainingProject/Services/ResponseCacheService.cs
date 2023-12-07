using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AspNetCoreTrainingProject.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string?> GetCachedResponseAsync(string cachedKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cachedKey);

            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }

        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);
            await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }
    }
}
