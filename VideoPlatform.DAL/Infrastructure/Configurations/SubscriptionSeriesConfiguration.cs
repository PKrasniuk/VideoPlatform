using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations
{
    internal class SubscriptionSeriesConfiguration : IEntityTypeConfiguration<SubscriptionSeries>
    {
        public void Configure(EntityTypeBuilder<SubscriptionSeries> builder)
        {
            builder.ToTable("SubscriptionSeries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.SeriesId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasIndex(x => new {x.UserId, x.SeriesId}).IsUnique().IsClustered(false);
        }
    }
}