using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum ExperimentType : byte
{
    [Description("Content")] Content = 1,
    [Description("Engagement Depth")] EngagementDepth = 2
}