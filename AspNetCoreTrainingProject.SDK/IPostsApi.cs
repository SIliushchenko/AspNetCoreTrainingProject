using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using Refit;
using AspNetCoreTrainingProject.Domain;

namespace AspNetCoreTrainingProject.SDK
{
    [Headers("Authorization: Bearer")]
    public interface IPostsApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<List<Post>>> GetAllAsync();

        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<Post>> GetAsync(Guid postId);

        [Post("/api/v1/posts")]
        Task<ApiResponse<PostResponse>> CreateAsync([Body]CreatePostRequest createPostRequest);

        [Put("/api/v1/posts")]
        Task<ApiResponse<Post>> UpdateAsync([Body] UpdatePostRequest updatePostRequest);

        [Delete("/api/v1/posts/{postId}")]
        Task<ApiResponse<string>> DeleteAsync(Guid postId);
    }
}
