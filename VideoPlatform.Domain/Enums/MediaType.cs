using System.ComponentModel;

namespace VideoPlatform.Domain.Enums
{
    public enum MediaType: byte
    {
        [Description("Video")]
        Video = 1,
        [Description("Podcast Audio")]
        PodcastAudio = 2,
        [Description("Embedded Media")]
        EmbeddedMedia = 3,
        [Description("Banner")]
        Banner = 4
    }
}