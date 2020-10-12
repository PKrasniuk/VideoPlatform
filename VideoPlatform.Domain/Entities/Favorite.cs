namespace VideoPlatform.Domain.Entities
{
    public class Favorite : Entity<int>
    {
        public long MediaId { get; set; }

        public int UserId { get; set; }

        public virtual Media Media { get; set; }

        public virtual AppUser User { get; set; }
    }
}