using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace VideoPlatform.Api.Infrastructure.HealthCheck
{
    /// <summary>
    /// SystemMemoryHealthCheck
    /// </summary>
    public class SystemMemoryHealthCheck : IHealthCheck
    {
        /// <summary>
        /// CheckHealth
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            var client = new MemoryMetricsClient();
            var metrics = client.GetMetrics();
            var percentUsed = 100 * metrics.Used / metrics.Total;

            var status = HealthStatus.Healthy;
            if (percentUsed > 80) 
                status = HealthStatus.Degraded;
            if (percentUsed > 90) 
                status = HealthStatus.Unhealthy;

            var data = new Dictionary<string, object>
            {
                {"Total", metrics.Total},
                {"Used", metrics.Used},
                {"Free", metrics.Free},
                {"Duration", metrics.Duration}
            };

            return await Task.FromResult(new HealthCheckResult(status, null, null, data));
        }
    }
}