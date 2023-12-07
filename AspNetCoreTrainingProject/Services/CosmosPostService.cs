using AspNetCoreTrainingProject.Domain;
using Cosmonaut;
using Cosmonaut.Extensions;

namespace AspNetCoreTrainingProject.Services
{
    public class CosmosPostService : IPostService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
        {
            _cosmosStore = cosmosStore ?? throw new ArgumentNullException(nameof(cosmosStore));
        }

        public Task<bool> CheckUserOwnsPostAsync(Guid id, string v)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            var cosmosPost = new CosmosPostDto
            {
                Id =  Guid.NewGuid().ToString(),
                Name = post.Name
            };

            var response = await _cosmosStore.AddAsync(cosmosPost);
            post.Id = Guid.Parse(cosmosPost.Id);
            return response.IsSuccess;
        }

        public async Task<bool> DeletePostAsync(Guid id)
        {
            var response = await _cosmosStore.RemoveByIdAsync(id.ToString(), id.ToString());
            return response.IsSuccess;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            var post = await _cosmosStore.FindAsync(id.ToString(), id.ToString());

            return (post == null ? null : new Post { Id = Guid.Parse(post.Id), Name = post.Name })!;
        }

        public async Task<List<Post>> GetPostsAsync(PaginationFilter? paginationFilter = null)
        {
           var posts = await _cosmosStore.Query().ToListAsync();

            return posts.Select(x => new Post { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            var cosmosPost = new CosmosPostDto
            {
                Id = postToUpdate.Id.ToString(),
                Name = postToUpdate.Name
            };

            var response = await _cosmosStore.UpdateAsync(cosmosPost);
            return response.IsSuccess;
        }
    }
}
