using MediatR;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Commands;

public class UpdateSettingCommand : BaseCommand<Unit>
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