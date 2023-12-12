using MediatR;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Commands;

public class UpdateSettingCommand : BaseCommand<Unit>, IRequest
{
    public UpdateSettingCommand()
    {
    }

    public UpdateSettingCommand(Setting entity)
    {
        Entity = entity;
    }

    public Setting Entity { get; }
}