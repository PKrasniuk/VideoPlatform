using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum InfoType : byte
{
    [Description("BaseDocument")] BaseDocument = 1,
    [Description("SupportDocument")] SupportDocument = 2
}