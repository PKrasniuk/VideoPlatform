using Microsoft.AspNetCore.Identity;

namespace VideoPlatform.Domain.Entities;

public class AppRole : IdentityRole<int>
{
    public string Description { get; set; }
}