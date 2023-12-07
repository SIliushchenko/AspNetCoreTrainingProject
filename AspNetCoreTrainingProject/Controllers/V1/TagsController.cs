using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTrainingProject.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route(ApiRoutes.Base)]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;

        public TagsController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        [Route("tags")]
        [Authorize(Policy = "MustWorkForGmail")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(tags);
        }

    }
}
