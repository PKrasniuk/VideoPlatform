using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);

        builder.HasIndex(x => x.Name).IsUnique().IsClustered(false);
    }
}