using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using Refit;

namespace AspNetCoreTrainingProject.SDK
{
    public interface IIdentityApi
    {
        [Post("/api/v1/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registrationRequest);

        [Post("/api/v1/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest loginRequest);

        [Post("/api/v1/identity/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshTokenRequest);
    }
}
