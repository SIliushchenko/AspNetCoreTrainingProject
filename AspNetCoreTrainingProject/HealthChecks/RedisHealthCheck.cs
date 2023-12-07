using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace AspNetCoreTrainingProject.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisHealthCheck(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var dataBase = _connectionMultiplexer.GetDatabase();
                dataBase.StringGet("health");
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(e.Message));
            }
        }
    }
}
