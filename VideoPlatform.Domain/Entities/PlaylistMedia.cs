namespace VideoPlatform.Domain.Entities;

public class PlaylistMedia : Entity<int>
{
    public long MediaId { get; set; }

    public int PlaylistId { get; set; }

    public virtual Media Media { get; set; }

    public virtual Playlist Playlist { get; set; }
}