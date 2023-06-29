using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace VideoPlatform.SchedulerService.Jobs;

public class BasedJob : IJob
{
    private readonly ILogger<BasedJob> _logger;

    public BasedJob(ILogger<BasedJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Test");

        return Task.CompletedTask;
    }
}