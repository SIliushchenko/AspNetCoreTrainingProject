using AspNetCoreTrainingProject.BackgroundTasks;
using AspNetCoreTrainingProject.Cache;
using AspNetCoreTrainingProject.Services;
using StackExchange.Redis;

namespace AspNetCoreTrainingProject.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            serviceCollection.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            serviceCollection.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.RedisConnection);
            serviceCollection.AddSingleton<IResponseCacheService, ResponseCacheService>();
            serviceCollection.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisCacheSettings.RedisConnection));
            //serviceCollection.AddSingleton<ICashService, RedisCacheService>();
            //serviceCollection.AddHostedService<RedisSubscriber>();
        }
    }
}
