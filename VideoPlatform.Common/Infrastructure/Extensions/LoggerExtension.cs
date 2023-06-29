using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace VideoPlatform.Common.Infrastructure.Extensions;

public static class LoggerExtension
{
    public static void AddLoggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        AppDomain.CurrentDomain.ProcessExit += (_, _) => Log.CloseAndFlush();

        services.AddSingleton(Log.Logger);
    }
}