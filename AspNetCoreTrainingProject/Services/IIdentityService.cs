using AspNetCoreTrainingProject.Domain;

namespace AspNetCoreTrainingProject.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
