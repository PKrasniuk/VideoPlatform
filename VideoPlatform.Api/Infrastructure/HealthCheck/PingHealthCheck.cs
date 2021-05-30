using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace VideoPlatform.Api.Infrastructure.HealthCheck
{
    /// <summary>
    /// PingHealthCheck
    /// </summary>
    public class PingHealthCheck : IHealthCheck
    {
        private readonly string _host;
        private readonly int _timeout;
        private readonly int _pingInterval;
        private DateTime _lastPingTime = DateTime.MinValue;
        private HealthCheckResult _lastPingResult = HealthCheckResult.Healthy();
        private readonly object _locker = new();

        /// <summary>
        /// PingHealthCheck Constructor
        /// </summary>
        /// <param name="host"></param>
        /// <param name="timeout"></param>
        /// <param name="pingInterval"></param>
        public PingHealthCheck(string host, int timeout, int pingInterval = 0)
        {
            _host = host;
            _timeout = timeout;
            _pingInterval = pingInterval;
        }

        private bool IsCacheExpired() => _pingInterval == 0 || _lastPingTime.AddSeconds(_pingInterval) <= DateTime.Now;

        /// <summary>
        /// CheckHealth
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            if (!IsCacheExpired())
                return await Task.FromResult(_lastPingResult);

            if (Monitor.TryEnter(_locker))
                try
                {
                    if (IsCacheExpired())
                        try
                        {
                            using var ping = new Ping();
                            _lastPingTime = DateTime.Now;

                            var reply = ping.Send(_host, _timeout);
                            if (reply != null && reply.Status != IPStatus.Success)
                                _lastPingResult = HealthCheckResult.Unhealthy();
                            else if (reply != null && reply.RoundtripTime >= _timeout)
                                _lastPingResult = HealthCheckResult.Degraded();
                            else
                                _lastPingResult = HealthCheckResult.Healthy();
                        }
                        catch
                        {
                            _lastPingResult = HealthCheckResult.Unhealthy();
                        }
                }
                finally
                {
                    Monitor.Exit(_locker);
                }

            return await Task.FromResult(_lastPingResult);
        }
    }
}