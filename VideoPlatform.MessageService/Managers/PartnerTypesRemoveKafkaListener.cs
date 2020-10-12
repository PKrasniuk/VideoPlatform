using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers
{
    /// <summary>
    /// PartnerTypesRemoveKafkaListener
    /// </summary>
    public sealed class PartnerTypesRemoveKafkaListener : KafkaListener
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PartnerTypesRemoveKafkaListener(IConsumerWrapper consumerWrapper, IServiceScopeFactory scopeFactory) : base(consumerWrapper)
        {
            MessageType = MessageType.PartnerTypesRemove;
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override async Task ProcessAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var model = JsonConvert.DeserializeObject<PartnerTypesRemoveModel>(message);
                using (var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPartnerTypesRepository>();

                    var partnerTypes = await repository.GetEntityAsync(x =>
                        x.PartnerId == model.PartnerId && x.Type.Equals(model.Type));
                    if (partnerTypes != null)
                    {
                        await repository.RemoveEntityAsync(partnerTypes.Id);
                    }
                }
            }
        }
    }
}