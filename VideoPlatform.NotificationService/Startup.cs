using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using VideoPlatform.Common.Infrastructure.Configurations;
using VideoPlatform.Common.Infrastructure.Extensions;
using VideoPlatform.Common.Infrastructure.Middleware;
using VideoPlatform.MessageService.Infrastructure.Extensions;
using VideoPlatform.NotificationService.Infrastructure.Extensions;

namespace VideoPlatform.NotificationService
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLoggerConfiguration(Configuration);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();
                //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                //.AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddEventBus(Configuration);
            services.AddSubscribes();

            services.AddValidatorsCollection();
            services.AddSwaggerConfiguration();

            services.AddCors();
            services.AddSignalRConfiguration();

            services.AddHsts(AdditionalConfig.ConfigureHsts);
            services.AddHttpsRedirection(AdditionalConfig.ConfigureHttpsRedirection);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            loggerFactory.AddSerilog();

            app.AddSwaggerBuilder();
            app.AddCorsBuilder();
            app.AddSignalRBuilder();
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}