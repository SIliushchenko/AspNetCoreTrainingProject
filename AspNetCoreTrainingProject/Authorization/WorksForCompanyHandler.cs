﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTrainingProject.Authorization
{
    public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorksForCompanyRequirement requirement)
        {
            var userEmailAddress = context?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if (userEmailAddress.EndsWith(requirement.DomainName))
            {
                context?.Succeed(requirement);
                return Task.CompletedTask;
            }

            context?.Fail();
            return Task.CompletedTask;
        }
    }
}
