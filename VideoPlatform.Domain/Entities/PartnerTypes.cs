using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities;

public class PartnerTypes : Entity<int>
{
    public int PartnerId { get; set; }

    public PartnerType Type { get; set; }

    public virtual Partner Partner { get; set; }
}