using Microsoft.OpenApi.Models;

namespace AspNetCoreTrainingProject.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tweet book API", Version = "v1" });

                var openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                x.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        openApiSecurityScheme,
                        new string[] { }
                    }
                });
            });
        }
    }
}
