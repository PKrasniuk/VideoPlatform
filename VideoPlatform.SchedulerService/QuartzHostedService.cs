using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using VideoPlatform.SchedulerService.Jobs;

namespace VideoPlatform.SchedulerService;

/// <summary>
///     QuartzHostedService
/// </summary>
public class QuartzHostedService(
    ISchedulerFactory schedulerFactory,
    IEnumerable<JobSchedule> jobSchedules,
    IJobFactory jobFactory)
    : IHostedService
{
    private IScheduler Scheduler { get; set; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Scheduler = await schedulerFactory.GetScheduler(cancellationToken);

        Scheduler.JobFactory = jobFactory;

        foreach (var jobSchedule in jobSchedules)
        {
            var job = CreateJob(jobSchedule);
            var trigger = CreateTrigger(jobSchedule);

            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
        }

        await Scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (Scheduler != null)
            await Scheduler.Shutdown(cancellationToken);
    }

    private static ITrigger CreateTrigger(JobSchedule schedule)
    {
        return TriggerBuilder
            .Create()
            .WithIdentity($"{schedule.JobType.FullName}.trigger")
            .WithCronSchedule(schedule.CronExpression)
            .WithDescription(schedule.CronExpression)
            .Build();
    }

    private static IJobDetail CreateJob(JobSchedule schedule)
    {
        var jobType = schedule.JobType;

        return JobBuilder
            .Create(jobType)
            .WithIdentity(jobType.FullName ?? throw new InvalidOperationException())
            .WithDescription(jobType.Name)
            .Build();
    }
}