namespace VideoPlatform.Domain.Entities;

public class Tool : Entity<int>
{
    public string Name { get; set; }

    public long? MediaId { get; set; }

    public int? SeriesId { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public virtual Media Media { get; set; }

    public virtual Series Series { get; set; }
}