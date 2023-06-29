using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Commands;

public class CreateSettingCommand : BaseCommand<Setting>
{
    public CreateSettingCommand()
    {
    }

    public CreateSettingCommand(Setting entity)
    {
        Entity = entity;
    }

    public Setting Entity { get; }
}