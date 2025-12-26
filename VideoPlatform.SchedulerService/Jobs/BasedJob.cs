using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace VideoPlatform.SchedulerService.Jobs;

public class BasedJob(ILogger<BasedJob> logger) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Test");

        return Task.CompletedTask;
    }
}