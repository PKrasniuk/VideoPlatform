using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using VideoPlatform.Api.Infrastructure.Extensions;
using VideoPlatform.BLL.Infrastructure.Extensions;
using VideoPlatform.Common.Infrastructure.Configurations;
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
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) =>
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLoggerConfiguration(Configuration);

            //services.AddMvc(options => options.Filters.Add(new CorsAuthorizationFilterFactory(ConfigurationConstants.DefaultCorsPolicyName)))
            //    .SetCompatibilityVersion(CompatibilityVersion.Latest)
            //    .AddFluentValidation()
            //    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            //    .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = true;
            });
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddResponseConfiguration();

            services.AddBusinessInfrastructureConfiguration(Configuration);

            services.AddCorsConfiguration(Configuration);

            services.AddValidatorsCollection();
            services.AddMappingConfiguration();

            services.AddSecurityConfiguration(Configuration);
            services.AddSwaggerConfiguration(Configuration);

            services.AddHealthCheck(Configuration);
            services.AddAppMetrics(Configuration);

            services.AddHsts(AdditionalConfig.ConfigureHsts);
            services.AddHttpsRedirection(AdditionalConfig.ConfigureHttpsRedirection);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            loggerFactory.AddSerilog();

            app.UseCors(ConfigurationConstants.DefaultCorsPolicyName);
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.AddBusinessInfrastructureBuilder(userManager, roleManager);

            app.AddSwaggerBuilder(Configuration);
            app.AddHealthChecksBuilder();
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
            app.UseHealthAllEndpoints();
        }
    }
}