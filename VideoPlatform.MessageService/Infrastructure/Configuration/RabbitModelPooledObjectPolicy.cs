using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using VideoPlatform.MessageService.Models;

namespace VideoPlatform.MessageService.Infrastructure.Configuration;

public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
{
    private readonly IConnection _connection;
    private readonly RabbitOptions _options;

    public RabbitModelPooledObjectPolicy(IOptions<RabbitOptions> optionsAccess)
    {
        _options = optionsAccess.Value;
        if (_options != null)
            _connection = GetConnection();
    }

    public IModel Create()
    {
        return _connection?.CreateModel();
    }

    public bool Return(IModel obj)
    {
        if (obj != null)
        {
            if (obj.IsOpen)
                return true;

            obj.Dispose();
        }

        return false;
    }

    private IConnection GetConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password,
                Port = _options.Port,
                VirtualHost = _options.VHost
            };

            return factory.CreateConnection();
        }
        catch
        {
            return null;
        }
    }
}