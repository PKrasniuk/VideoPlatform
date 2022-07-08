using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using VideoPlatform.Common.Infrastructure.Extensions;
using VideoPlatform.Common.Models.ConfigurationModels;

namespace VideoPlatform.AuthenticationService
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args) => await CreateWebHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .InitAppConfiguration(new TransientFaultHandlingOptions())
                .ConfigureWebHostDefaults(hostBuilder => hostBuilder.UseStartup<Startup>());
            host.UseSerilog();
            return host;
        }
    }
}
