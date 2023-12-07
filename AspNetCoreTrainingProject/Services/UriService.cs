using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Contracts.V1.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace AspNetCoreTrainingProject.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostUri(string postId)
        {
            return new Uri(_baseUri + "/" + ApiRoutes.Base + "posts/" + postId);
        }

        public Uri GetAllPostsUri(PaginationQuery? paginationQuery = null)
        {
            var uri = new Uri(_baseUri);
            if (paginationQuery == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
