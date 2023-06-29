using System.Collections.Generic;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.CQRS.Queries;

public class SettingsQuery : BaseQuery<IEnumerable<Setting>>
{
}