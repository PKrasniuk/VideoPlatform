using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Interfaces;

public interface IProducerWrapper
{
    Task<DeliveryResult<string, string>> WriteMessage(string message, MessageType messageType,
        CancellationToken cancellationToken = default);
}