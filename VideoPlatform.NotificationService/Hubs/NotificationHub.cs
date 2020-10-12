using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VideoPlatform.NotificationService.Hubs
{
    /// <summary>
    /// NotificationHub
    /// </summary>
    public class NotificationHub : Hub
    {
        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("OnReceiveMessage", message);
        }

        /// <summary>
        /// SendMessageToCaller
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToCallerAsync(string message)
        {
            await Clients.Caller.SendAsync("OnReceiveMessage", message);
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageWithKeyAsync(string key, string message)
        {
            await Clients.All.SendAsync(key, message);
        }
    }
}