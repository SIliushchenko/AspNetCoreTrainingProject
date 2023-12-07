using AspNetCoreTrainingProject.Domain;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;

namespace AspNetCoreTrainingProject.Installers
{
    public class CosmosIntaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //var cososStoreSettings = new CosmosStoreSettings(configuration["CosmosSettings:DatabaseName"],
            //    configuration["CosmosSettings:AccountUri"],
            //    configuration["CosmosSettings:AccountKey"],
            //    new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp });

            //serviceCollection.AddCosmosStore<CosmosPostDto>(cososStoreSettings);
        }
    }
}
