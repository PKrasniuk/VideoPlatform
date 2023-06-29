using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class PartnerMediaConfiguration : IEntityTypeConfiguration<PartnerMedia>
{
    public void Configure(EntityTypeBuilder<PartnerMedia> builder)
    {
        builder.ToTable("PartnerMedia");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.MediaId).IsRequired();
        builder.Property(x => x.PartnerId).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.IsExpired).IsRequired();

        builder.HasIndex(x => new { x.MediaId, x.PartnerId }).IsUnique().IsClustered(false);
    }
}