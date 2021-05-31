using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers
{
    /// <summary>
    /// PartnerTypesRemoveListener
    /// </summary>
    public sealed class PartnerTypesRemoveListener : RabbitListener
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PartnerTypesRemoveListener(IOptions<RabbitOptions> optionsAccess, IServiceScopeFactory scopeFactory) : base(optionsAccess)
        {
            MessageType = MessageType.PartnerTypesRemove;
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override bool Process(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var model = JsonConvert.DeserializeObject<PartnerTypesRemoveModel>(message);
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IPartnerTypesRepository>();

                AsyncHelper.RunSync(async () =>
                {
                    var partnerTypes = await repository.GetEntityAsync(x =>
                        x.PartnerId == model.PartnerId && x.Type.Equals(model.Type));
                    if (partnerTypes != null) 
                        await repository.RemoveEntityAsync(partnerTypes.Id);
                });

                return true;
            }

            return false;
        }
    }
}