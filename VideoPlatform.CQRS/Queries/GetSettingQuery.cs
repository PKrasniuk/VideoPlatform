using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Queries;

public class GetSettingQuery : BaseQuery<Setting>
{
    public GetSettingQuery()
    {
    }

    public GetSettingQuery(short settingId)
    {
        SettingId = settingId;
    }

    public short SettingId { get; }
}