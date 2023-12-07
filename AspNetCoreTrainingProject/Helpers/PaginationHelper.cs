using AspNetCoreTrainingProject.Contracts.V1.Requests.Queries;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using AspNetCoreTrainingProject.Domain;
using AspNetCoreTrainingProject.Services;

namespace AspNetCoreTrainingProject.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter paginationFilter, List<T> posts)
        {
            var nextPage = paginationFilter.PageNumber >= 1
                ? uriService.GetAllPostsUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
                : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService.GetAllPostsUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = posts,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : null,
                NextPage = posts.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
