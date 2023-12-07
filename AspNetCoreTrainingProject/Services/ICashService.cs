namespace AspNetCoreTrainingProject.Services
{
    public interface ICashService
    {
        Task<string?> GetCacheValueAsync(string key);

        Task SetCacheValueAsync(string key, string value);
    }
}
