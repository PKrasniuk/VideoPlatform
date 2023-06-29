namespace VideoPlatform.MessageService.Models;

public class MessageParameters
{
    public string ExchangeName { get; set; }

    public string QueueName { get; set; }

    public string RouteKey { get; set; }
}