using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using VideoPlatform.Api.Infrastructure.Extensions;
using VideoPlatform.BLL.Infrastructure.Extensions;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Extensions;
using VideoPlatform.Common.Infrastructure.Middleware;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api
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

            services.AddMvc(options => options.Filters.Add(new CorsAuthorizationFilterFactory(ConfigurationConstants.DefaultCorsPolicyName)))
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddResponseConfiguration();

            services.AddBusinessInfrastructureConfiguration(_appConfiguration);

            services.AddCorsConfiguration(_appConfiguration);

            services.AddValidatorsCollection();
            services.AddMappingConfiguration();

            services.AddSecurityConfiguration(_appConfiguration);
            services.AddSwaggerConfiguration(_appConfiguration);

            services.AddHealthCheck(_appConfiguration);
            services.AddAppMetrics(_appConfiguration);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
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

            app.UseCors(ConfigurationConstants.DefaultCorsPolicyName);
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.AddBusinessInfrastructureBuilder(_appConfiguration, userManager, roleManager);

            app.AddSwaggerBuilder(_appConfiguration);
            app.AddHealthChecksBuilder();
            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
            app.UseHealthAllEndpoints();
            app.UseMvc();
        }
    }
}