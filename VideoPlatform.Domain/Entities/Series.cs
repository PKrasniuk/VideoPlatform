using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideoPlatform.Domain.Entities
{
    public class Series : Entity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public virtual ICollection<Media> Media { get; set; } = new Collection<Media>();

        public virtual ICollection<SubscriptionSeries> SubscriptionSeries { get; set; } = new Collection<SubscriptionSeries>();

        public virtual ICollection<Tool> Tools { get; set; } = new Collection<Tool>();
    }
}