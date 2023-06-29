using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideoPlatform.Domain.Entities;

public class Partner : Entity<int>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Logo { get; set; }

    public bool ShowOnPartnerPage { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<Media> Media { get; set; } = new Collection<Media>();

    public virtual ICollection<PartnerMedia> PartnerMedia { get; set; } = new Collection<PartnerMedia>();

    public virtual ICollection<PartnerTypes> PartnerTypes { get; set; } = new Collection<PartnerTypes>();
}