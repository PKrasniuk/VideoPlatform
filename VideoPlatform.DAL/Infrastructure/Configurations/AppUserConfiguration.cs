using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations;

internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(FieldConstants.QuarterFieldLength);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
        builder.Property(x => x.PartnerId).IsRequired(false);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired().HasDefaultValueSql("GETUTCDATE()");
        builder.Property(x => x.Email).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(FieldConstants.PhoneFieldLength);

        builder.HasIndex(x => x.PartnerId).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.Status).IsUnique(false).IsClustered(false);
        builder.HasIndex(x => x.Email).IsUnique().IsClustered(false);
    }
}