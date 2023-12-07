using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTrainingProject.Authorization
{
    public class WorksForCompanyRequirement : IAuthorizationRequirement
    {
        public WorksForCompanyRequirement(string domainName)
        {
            DomainName = domainName;
        }

        public string DomainName { get; }
    }
}
