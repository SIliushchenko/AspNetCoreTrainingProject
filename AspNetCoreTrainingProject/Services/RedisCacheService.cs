using StackExchange.Redis;

namespace AspNetCoreTrainingProject.Services
{
    public class RedisCacheService : ICashService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string?> GetCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();

            return await db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();

            await db.StringSetAsync(key, value);
        }
    }
}
