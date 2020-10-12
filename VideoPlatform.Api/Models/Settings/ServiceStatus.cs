using System.Collections.Generic;
using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace VideoPlatform.Api.Models.Settings
{
    /// <summary>
    /// ServiceStatus
    /// </summary>
    public class ServiceStatus
    {
        /// <summary>
        /// Service
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public HealthStatus Status { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public List<KeyValuePair<string, object>> Data { get; set; }
    }
}