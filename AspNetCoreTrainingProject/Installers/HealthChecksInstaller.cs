using AspNetCoreTrainingProject.Data;
using AspNetCoreTrainingProject.HealthChecks;

namespace AspNetCoreTrainingProject.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
