using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTrainingProject.Controllers.V1
{
    [ApiKeyAuth]
    [Route(ApiRoutes.Base)]
    public class SecretController : ControllerBase
    {
        [HttpGet]
        [Route("Secret")]
        public IActionResult GetSecret()
        {
            return Ok("I have no secret!");
        }
    }
}
