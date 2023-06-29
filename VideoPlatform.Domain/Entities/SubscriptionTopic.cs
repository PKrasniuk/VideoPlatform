namespace VideoPlatform.Domain.Entities;

public class SubscriptionTopic : Entity<int>
{
    public int TopicId { get; set; }

    public int UserId { get; set; }

    public virtual Topic Topic { get; set; }

    public virtual AppUser User { get; set; }
}