namespace AspNetCoreTrainingProject.Cache
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }

        public string RedisConnection { get; set; } = null!;
    }
}
