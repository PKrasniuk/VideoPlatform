using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities;

public class Media : Entity<long>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int UploadUserId { get; set; }

    public int? PublishUserId { get; set; }

    public int? SeriesId { get; set; }

    public int? TopicId { get; set; }

    public int? SourceId { get; set; }

    public string EmbeddedCode { get; set; }

    public string Url { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DatePublished { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public bool IsPrivate { get; set; }

    public bool? IsSharingAllowed { get; set; }

    public string Thumbnail { get; set; }

    public string Logo { get; set; }

    public MediaStatus Status { get; set; }

    public MediaType Type { get; set; }

    public virtual AppUser PublishUser { get; set; }

    public virtual Series Series { get; set; }

    public virtual Partner Source { get; set; }

    public virtual Topic Topic { get; set; }

    public virtual AppUser UploadUser { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new Collection<Favorite>();

    public virtual ICollection<MediaTag> MediaTag { get; set; } = new Collection<MediaTag>();

    public virtual ICollection<PartnerMedia> PartnerMedia { get; set; } = new Collection<PartnerMedia>();

    public virtual ICollection<PlaylistMedia> PlaylistMedia { get; set; } = new Collection<PlaylistMedia>();

    public virtual ICollection<Tool> Tools { get; set; } = new Collection<Tool>();
}