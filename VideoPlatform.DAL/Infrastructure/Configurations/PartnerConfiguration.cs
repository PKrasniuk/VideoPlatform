using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.ToTable("Partners");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(FieldConstants.QuarterFieldLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
        builder.Property(x => x.Logo).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
        builder.Property(x => x.ShowOnPartnerPage).IsRequired();
        builder.Property(x => x.IsArchived).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique().IsClustered(false);
    }
}