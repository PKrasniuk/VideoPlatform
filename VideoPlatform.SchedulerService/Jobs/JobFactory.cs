using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace VideoPlatform.SchedulerService.Jobs;

public class JobFactory(IServiceProvider serviceProvider) : IJobFactory
{
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return (serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob)!;
    }

    public void ReturnJob(IJob job)
    {
    }
}