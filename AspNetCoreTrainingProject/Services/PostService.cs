using AspNetCoreTrainingProject.Data;
using AspNetCoreTrainingProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTrainingProject.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }


        public async Task<List<Post>> GetPostsAsync(PaginationFilter? paginationFilter = null)
        {
            if (paginationFilter == null)
            {
                return await _dataContext.Posts.ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return await _dataContext.Posts.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return (await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == id))!;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.Update(postToUpdate);
            var updateCount = await _dataContext.SaveChangesAsync();

            return updateCount > 0;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            var added = await _dataContext.SaveChangesAsync();
            return added > 0;
        }

        public async Task<bool> DeletePostAsync(Guid id)
        {
            var post = await GetPostByIdAsync(id);

            if (post is null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleteCount = await _dataContext.SaveChangesAsync();
            return deleteCount > 0;
        }

        public async Task<bool> CheckUserOwnsPostAsync(Guid id, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (post is null)
                return false;

            if(post.UserId != userId)
                return false;

            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }
    }
}
