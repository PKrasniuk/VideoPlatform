using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.NotificationService.Hubs;
using Xunit;

namespace VideoPlatform.Tests.IntegrationTests
{
    public class NotificationServiceIntegrationTests
    {
        private const string Message = "Integration Testing in Microsoft AspNetCore SignalR";
        private readonly HubConnection _connection;

        public NotificationServiceIntegrationTests()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSignalR();
                })
                .Configure(app =>
                {
                    app.UseSignalR(routes => routes.MapHub<NotificationHub>("/api/notification"));
                });

            var server = new TestServer(webHostBuilder);
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost/api/notification",
                    o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();
        }

        [Fact]
        public async Task SignalRSendMessageTestAsync()
        {
            var echo = string.Empty;

            _connection.On<string>("OnReceiveMessage", msg =>
            {
                echo = msg;
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendMessageAsync", Message);

            echo.Should().Be(Message);
        }

        [Fact]
        public async Task SendMessageToCallerTestAsync()
        {
            var echo = string.Empty;

            _connection.On<string>("OnReceiveMessage", msg =>
            {
                echo = msg;
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendMessageToCallerAsync", Message);

            echo.Should().Be(Message);
        }

        [Fact]
        public async Task SendMessageWithKeyTestAsync()
        {
            var echo = string.Empty;
            const string testKey = "testKey";

            _connection.On<string>(testKey, msg =>
            {
                echo = msg;
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendMessageWithKeyAsync", testKey, Message);

            echo.Should().Be(Message);
        }
    }
}