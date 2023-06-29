using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class PlaylistMediaConfiguration : IEntityTypeConfiguration<PlaylistMedia>
{
    public void Configure(EntityTypeBuilder<PlaylistMedia> builder)
    {
        builder.ToTable("PlaylistMedia");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.MediaId).IsRequired();
        builder.Property(x => x.PlaylistId).IsRequired();

        builder.HasIndex(x => new { x.MediaId, x.PlaylistId }).IsUnique().IsClustered(false);
    }
}