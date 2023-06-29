using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum MediaStatus
{
    [Description("Published")] Published = 1,
    [Description("Draft")] Draft = 2,
    [Description("Archived")] Archived = 3
}