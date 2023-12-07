namespace AspNetCoreTrainingProject.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}
