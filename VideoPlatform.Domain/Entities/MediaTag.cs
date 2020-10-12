namespace VideoPlatform.Domain.Entities
{
    public class MediaTag : Entity<int>
    {
        public long MediaId { get; set; }

        public int TagId { get; set; }

        public virtual Media Media { get; set; }

        public virtual Tag Tag { get; set; }
    }
}