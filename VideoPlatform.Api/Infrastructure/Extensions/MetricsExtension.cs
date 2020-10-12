using System;
using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.InfluxDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        public static IServiceCollection AddAppMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            var metrics = AppMetrics.CreateDefaultBuilder().Configuration.Configure(options =>
                {
                    options.Enabled = true;
                    options.ReportingEnabled = true;
                })
                .Report.ToInfluxDb(
                    options =>
                    {
                        options.InfluxDb.BaseUri = new Uri(configuration["Metrics:InfluxDBUrl"]);
                        options.InfluxDb.Database = configuration["Metrics:MetricsDBName"];
                        options.InfluxDb.CreateDataBaseIfNotExists = true;
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                        options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
                        options.Filter = new MetricsFilter().WhereType(MetricType.Timer);
                        options.FlushInterval = TimeSpan.FromSeconds(20);
                    })
                .Build();

            services.AddMetrics(metrics)
                .AddMetricsTrackingMiddleware()
                .AddMetricsReportingHostedService()
                .AddMetricsEndpoints()
                .AddHealth();

            return services;
        }
    }
}