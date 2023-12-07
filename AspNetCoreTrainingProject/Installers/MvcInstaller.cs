using AspNetCoreTrainingProject.Options;
using AspNetCoreTrainingProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AspNetCoreTrainingProject.Authorization;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using AspNetCoreTrainingProject.BackgroundTasks;

namespace AspNetCoreTrainingProject.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            serviceCollection.AddSingleton(jwtSettings);
            serviceCollection.AddScoped<IIdentityService, IdentityService>();
            serviceCollection.AddControllersWithViews();
            serviceCollection.AddAutoMapper(typeof(Program));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            serviceCollection.AddSingleton(tokenValidationParameters);

            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            serviceCollection.AddAuthorization(option =>
            {
                option.AddPolicy("MustWorkForGmail", policy =>
                {
                    policy.AddRequirements(new WorksForCompanyRequirement("gmail.com"));
                });
            });
            serviceCollection.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();

            serviceCollection.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var absoluteUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });
        }
    }
}
