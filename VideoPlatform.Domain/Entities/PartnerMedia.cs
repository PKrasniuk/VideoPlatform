using System;

namespace VideoPlatform.Domain.Entities;

public class PartnerMedia : Entity<int>
{
    public long MediaId { get; set; }

    public int PartnerId { get; set; }

    public string Email { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsExpired { get; set; }

    public virtual Media Media { get; set; }

    public virtual Partner Partner { get; set; }
}