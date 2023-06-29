using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class MediaTagConfiguration : IEntityTypeConfiguration<MediaTag>
{
    public void Configure(EntityTypeBuilder<MediaTag> builder)
    {
        builder.ToTable("MediaTags");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.MediaId).IsRequired();
        builder.Property(x => x.TagId).IsRequired();

        builder.HasIndex(x => new { x.MediaId, x.TagId }).IsUnique().IsClustered(false);
    }
}