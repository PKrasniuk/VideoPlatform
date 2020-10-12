using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using VideoPlatform.SchedulerService.Jobs;

namespace VideoPlatform.SchedulerService.Infrastructure.Extensions
{
    public static class SchedulerExtension
    {
        public static IServiceCollection AddSchedulerConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<BasedJob>();

            services.AddSingleton(new JobSchedule(jobType: typeof(BasedJob), cronExpression: "0/10 * * * * ?"));

            return services;
        }
    }
}