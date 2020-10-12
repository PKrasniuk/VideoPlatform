using System.ComponentModel;

namespace VideoPlatform.Domain.Enums
{
    public enum UserStatus : byte
    {
        [Description("Active")]
        Active = 1,
        [Description("Invited")]
        Invited = 2,
        [Description("Suspended")]
        Suspended = 3
    }
}