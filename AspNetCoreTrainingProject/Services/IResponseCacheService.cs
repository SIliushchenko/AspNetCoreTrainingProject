namespace AspNetCoreTrainingProject.Services
{
    public interface IResponseCacheService
    {
        Task<string?> GetCachedResponseAsync(string cachedKey);

        Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);
    }
}
