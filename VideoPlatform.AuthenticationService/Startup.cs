using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using VideoPlatform.AuthenticationService.Infrastructure.Extensions;
using VideoPlatform.Common.Infrastructure.Configurations;
using VideoPlatform.Common.Infrastructure.Extensions;

namespace VideoPlatform.AuthenticationService;

/// <summary>
///     Startup
/// </summary>
public class Startup
{
    /// <summary>
    ///     Startup constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    private IConfiguration Configuration { get; }

    /// <summary>
    ///     ConfigureServices
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLoggerConfiguration(Configuration);

        services.AddMvc();

        services.AddIdentityServerConfiguration(Configuration);

        services.AddHsts(AdditionalConfig.ConfigureHsts);
        services.AddHttpsRedirection(AdditionalConfig.ConfigureHttpsRedirection);
    }

    /// <summary>
    ///     Configure
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        loggerFactory.AddSerilog();

        app.UseIdentityServer();

        app.UseStaticFiles();
    }
}