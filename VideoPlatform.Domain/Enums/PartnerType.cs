using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum PartnerType : byte
{
    [Description("Content")] Content = 1,
    [Description("Distribution")] Distribution = 2,
    [Description("Sponsorship")] Sponsorship = 3
}