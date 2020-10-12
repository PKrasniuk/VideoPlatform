using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? PartnerId { get; set; }

        public UserStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Partner Partner { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; } = new Collection<Experiment>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new Collection<Favorite>();

        public virtual ICollection<Media> PublishMedia { get; set; } = new Collection<Media>();

        public virtual ICollection<Media> UploadMedia { get; set; } = new Collection<Media>();

        public virtual ICollection<Playlist> Playlist { get; set; } = new Collection<Playlist>();

        public virtual ICollection<SubscriptionSeries> SubscriptionSeries { get; set; } = new Collection<SubscriptionSeries>();

        public virtual ICollection<SubscriptionTopic> SubscriptionTopics { get; set; } = new Collection<SubscriptionTopic>();
    }
}