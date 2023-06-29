using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum UserType : byte
{
    [Description("Admin")] Admin = 1,
    [Description("Admin-Editor")] AdminEditor = 2,
    [Description("Partner-User")] PartnerUser = 3,
    [Description("User")] User = 4
}