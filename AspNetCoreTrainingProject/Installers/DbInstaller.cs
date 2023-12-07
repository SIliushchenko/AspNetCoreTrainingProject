using AspNetCoreTrainingProject.Data;
using AspNetCoreTrainingProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTrainingProject.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            serviceCollection.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
            serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
            serviceCollection.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            serviceCollection.AddScoped<IPostService, PostService>();
            //serviceCollection.AddSingleton<IPostService, CosmosPostService>();
        }
    }
}
