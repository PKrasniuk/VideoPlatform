using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideoPlatform.Domain.Entities;

public class Playlist : Entity<int>
{
    public string Name { get; set; }

    public int UserId { get; set; }

    public virtual AppUser User { get; set; }

    public virtual ICollection<PlaylistMedia> PlaylistMedia { get; set; } = new Collection<PlaylistMedia>();
}