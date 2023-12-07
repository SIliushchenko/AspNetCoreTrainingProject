using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using AspNetCoreTrainingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTrainingProject.Controllers.V1
{
    [Route(ApiRoutes.Base + "Identity/")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(new AuthFailedResponse
                {
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if(!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    ErrorMessages = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    ErrorMessages = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    ErrorMessages = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
