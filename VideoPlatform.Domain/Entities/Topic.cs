using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideoPlatform.Domain.Entities;

public class Topic : Entity<int>
{
    public int? ParentId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Logo { get; set; }

    public virtual Topic Parent { get; set; }

    public virtual ICollection<Topic> InverseParent { get; set; } = new Collection<Topic>();

    public virtual ICollection<Media> Media { get; set; } = new Collection<Media>();

    public virtual ICollection<SubscriptionTopic> SubscriptionTopics { get; set; } =
        new Collection<SubscriptionTopic>();
}