using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("Media");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
        builder.Property(x => x.UploadUserId).IsRequired();
        builder.Property(x => x.PublishUserId).IsRequired(false);
        builder.Property(x => x.SeriesId).IsRequired(false);
        builder.Property(x => x.TopicId).IsRequired(false);
        builder.Property(x => x.SourceId).IsRequired(false);
        builder.Property(x => x.EmbeddedCode).IsRequired().HasMaxLength(FieldConstants.BigFieldLength);
        builder.Property(x => x.Url).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
        builder.Property(x => x.DateCreated).IsRequired().HasDefaultValueSql("GETUTCDATE()");
        builder.Property(x => x.DatePublished).IsRequired(false);
        builder.Property(x => x.ActiveFrom).IsRequired(false);
        builder.Property(x => x.ActiveTo).IsRequired(false);
        builder.Property(x => x.IsPrivate).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsSharingAllowed).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.Thumbnail).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
        builder.Property(x => x.Logo).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Type).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique().IsClustered(false);
        builder.HasIndex(x => x.UploadUserId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.PublishUserId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.SeriesId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.TopicId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.SourceId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.DatePublished).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.Status).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.Type).IsUnique(false).IsClustered(false);
    }
}