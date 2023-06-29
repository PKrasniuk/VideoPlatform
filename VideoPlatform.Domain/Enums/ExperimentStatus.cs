using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum ExperimentStatus : byte
{
    [Description("Active")] Active = 1,
    [Description("Completed")] Completed = 2,
    [Description("Draft")] Draft = 3,
    [Description("Paused")] Paused = 4
}