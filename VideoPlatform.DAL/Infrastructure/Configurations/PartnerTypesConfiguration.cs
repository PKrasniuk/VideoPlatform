using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations
{
    internal class PartnerTypesConfiguration : IEntityTypeConfiguration<PartnerTypes>
    {
        public void Configure(EntityTypeBuilder<PartnerTypes> builder)
        {
            builder.ToTable("PartnerTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.PartnerId).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasIndex(x => new {x.PartnerId, x.Type}).IsUnique().ForSqlServerIsClustered(false);
        }
    }
}