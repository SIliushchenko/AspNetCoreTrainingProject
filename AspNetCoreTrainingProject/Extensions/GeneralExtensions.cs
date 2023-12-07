using System.Text.Json;
using AspNetCoreTrainingProject.Contracts.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace AspNetCoreTrainingProject.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.SingleOrDefault(x => x.Type == "id")!.Value;
        }

        public static void UseHealthChecks(this WebApplication webApplication)
        {
            webApplication.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description!
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });
        }
    }
}
