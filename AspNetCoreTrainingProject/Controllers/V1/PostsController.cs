using AspNetCoreTrainingProject.Cache;
using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Requests.Queries;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using AspNetCoreTrainingProject.Domain;
using AspNetCoreTrainingProject.Extensions;
using AspNetCoreTrainingProject.Helpers;
using AspNetCoreTrainingProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTrainingProject.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route(ApiRoutes.Base)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostsController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [Route("posts")]
        //[Cached(600)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var posts = await _postService.GetPostsAsync(paginationFilter);
            var paginationResponse = new PagedResponse<Post>(posts);
            if (paginationQuery == null || paginationQuery.PageNumber < 1 || paginationQuery.PageSize < 1)
            {
                return Ok(paginationResponse);
            }

            paginationResponse = PaginationHelper.CreatePaginatedResponse(_uriService, paginationFilter, posts);

            return Ok(paginationResponse);
        }


        [Route("posts")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostRequest updatePostRequest)
        {
            var userOwnsPost = await _postService.CheckUserOwnsPostAsync(updatePostRequest.Id, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { Error = "You do not own this post!" });
            }

            var post = await _postService.GetPostByIdAsync(updatePostRequest.Id);
            post.Name = updatePostRequest.Name;

            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
                return Ok(new Response<Post?>(post));

            return NotFound();
        }

        [Route("posts/{postId}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post is null)
            {
                return NotFound();
            }
            return Ok(new Response<Post?>(post));
        }

        [Route("posts/{postId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.CheckUserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { Error = "You do not own this post!" });
            }

            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [Route("posts")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(t => new Tag { Name = t }).ToList()
            };

            await _postService.CreatePostAsync(post);

            var locationUri = _uriService.GetPostUri(post.Id.ToString());
            var response = new PostResponse { Id = post.Id };

            return Created(locationUri, new Response<PostResponse>(response));
        }
    }
}
