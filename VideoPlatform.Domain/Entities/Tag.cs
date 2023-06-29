using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideoPlatform.Domain.Entities;

public class Tag : Entity<int>
{
    public string Name { get; set; }

    public virtual ICollection<MediaTag> MediaTag { get; set; } = new Collection<MediaTag>();
}