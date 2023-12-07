using AspNetCoreTrainingProject.Contracts.V1.Requests.Queries;
using AspNetCoreTrainingProject.Domain;
using AutoMapper;

namespace AspNetCoreTrainingProject.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
