using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.CQRS.Commands;
using VideoPlatform.CQRS.Queries;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Managers
{
    public class SettingManager : ISettingManager
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IMediator _mediator;

        public SettingManager(ISettingsRepository settingsRepository, IMediator mediator)
        {
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<ICollection<Setting>> GetSettingsAsync(CancellationToken cancellationToken)
        {
            return await _settingsRepository.GetEntitiesAsync(null, cancellationToken);
        }

        public async Task<ICollection<Setting>> GetSettingsCQRSAsync(CancellationToken cancellationToken)
        {
            return (ICollection<Setting>) await _mediator.Send(new SettingsQuery(), cancellationToken);
        }

        public async Task<Setting> GetSettingCQRSAsync(short id)
        {
            return await _mediator.Send(new GetSettingQuery(id));
        }

        public async Task<Setting> AddSettingCQRSAsync(Setting entity)
        {
            return await _mediator.Send(new CreateSettingCommand(entity));
        }

        public async Task UpdateSettingCQRSAsync(Setting entity)
        {
            await _mediator.Send(new UpdateSettingCommand(entity));
        }

        public async Task RemoveSettingCQRSAsync(short id)
        {
            await _mediator.Send(new RemoveSettingCommand(id));
        }
    }
}