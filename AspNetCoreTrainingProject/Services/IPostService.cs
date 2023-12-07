using AspNetCoreTrainingProject.Domain;

namespace AspNetCoreTrainingProject.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync(PaginationFilter? paginationFilter = null);
        Task<Post> GetPostByIdAsync(Guid id);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(Guid id);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> CheckUserOwnsPostAsync(Guid id, string userId);
        Task<List<Tag>> GetAllTagsAsync();
    }
}
