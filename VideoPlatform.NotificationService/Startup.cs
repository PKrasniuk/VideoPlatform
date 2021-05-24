using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
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
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLoggerConfiguration(_appConfiguration);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();
                //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                //.AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddEventBus(_appConfiguration);
            services.AddSubscribes();

            services.AddValidatorsCollection();
            services.AddSwaggerConfiguration();

            services.AddCors();
            services.AddSignalRConfiguration();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (env.IsDevelopment())
            {
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change
                // this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddSerilog();

            app.AddSwaggerBuilder();
            app.AddCorsBuilder();
            app.AddSignalRBuilder();
            app.UseMvc();
        }
    }
}