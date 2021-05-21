using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations
{
    internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.ParentId).IsRequired(false);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(FieldConstants.QuarterFieldLength);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(FieldConstants.BigFieldLength);
            builder.Property(x => x.Logo).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);

            builder.HasIndex(x => x.ParentId).IsUnique(false).IsClustered(false);
            builder.HasIndex(x => x.Name).IsUnique().IsClustered(false);
        }
    }
}