using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VideoPlatform.CQRS.Commands;
using VideoPlatform.CQRS.Queries;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Handlers;

public class SettingsHandler(ISettingsRepository settingsRepository) :
    IRequestHandler<GetSettingQuery, Setting>,
    IRequestHandler<SettingsQuery, IEnumerable<Setting>>,
    IRequestHandler<CreateSettingCommand, Setting>,
    IRequestHandler<UpdateSettingCommand>,
    IRequestHandler<RemoveSettingCommand>
{
    private readonly ISettingsRepository _settingsRepository =
        settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));

    public async Task<Setting> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
    {
        return await _settingsRepository.CreateEntityAsync(request.Entity, cancellationToken);
    }

    public async Task<Setting> Handle(GetSettingQuery request, CancellationToken cancellationToken)
    {
        return await _settingsRepository.GetEntityByIdAsync(request.SettingId, cancellationToken);
    }

    public async Task Handle(RemoveSettingCommand request, CancellationToken cancellationToken)
    {
        await _settingsRepository.RemoveEntityAsync(request.Id, cancellationToken);
    }

    public async Task<IEnumerable<Setting>> Handle(SettingsQuery request, CancellationToken cancellationToken)
    {
        return await _settingsRepository.GetEntitiesAsync(null, cancellationToken);
    }

    public async Task Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        await _settingsRepository.UpdateEntityAsync(request.Entity, cancellationToken);
    }
}