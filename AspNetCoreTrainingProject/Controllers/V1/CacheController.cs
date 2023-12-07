using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Filters;
using AspNetCoreTrainingProject.Model;
using AspNetCoreTrainingProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspNetCoreTrainingProject.Controllers.V1
{
    [Route(ApiRoutes.Base)]
    public class CacheController : ControllerBase
    {
        private readonly ICashService _redisCacheService;

        public CacheController(ICashService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpGet]
        [Route("GetCache/{key}")]
        public async Task<IActionResult> GetCache(string key)
        {
            var value = await _redisCacheService.GetCacheValueAsync(key);
            if (value == null)
            {
                return NoContent();
            }

            return Ok(value);
        }

        [HttpPost]
        [Route("SetCache")]
        public async Task<IActionResult> SetCache([FromBody]SetCacheRequest request)
        {
            await _redisCacheService.SetCacheValueAsync(request.Key, request.Value);

            return Ok();
        }
    }
}
