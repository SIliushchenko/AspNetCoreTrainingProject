using AspNetCoreTrainingProject.Contracts.V1.Requests.Queries;

namespace AspNetCoreTrainingProject.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);

        Uri GetAllPostsUri(PaginationQuery? paginationQuery = null);
    }
}
