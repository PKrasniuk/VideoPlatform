using MediatR;

namespace VideoPlatform.CQRS.Commands;

public class RemoveSettingCommand : BaseCommand<Unit>
{
    public RemoveSettingCommand()
    {
    }

    public RemoveSettingCommand(short id)
    {
        Id = id;
    }

    public short Id { get; }
}