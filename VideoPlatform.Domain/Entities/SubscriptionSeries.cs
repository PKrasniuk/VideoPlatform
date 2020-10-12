namespace VideoPlatform.Domain.Entities
{
    public class SubscriptionSeries : Entity<int>
    {
        public int SeriesId { get; set; }

        public int UserId { get; set; }
        
        public virtual Series Series { get; set; }

        public virtual AppUser User { get; set; }
    }
}