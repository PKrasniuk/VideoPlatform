using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations
{
    internal class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable("Tools");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(FieldConstants.HalfFieldLength);
            builder.Property(x => x.MediaId).IsRequired(false);
            builder.Property(x => x.SeriesId).IsRequired(false);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(FieldConstants.BaseFieldLength);

            builder.HasIndex(x => x.Name).IsUnique().ForSqlServerIsClustered(false);
            builder.HasIndex(x => x.MediaId).IsUnique(false).ForSqlServerIsClustered(false);
            builder.HasIndex(x => x.SeriesId).IsUnique(false).ForSqlServerIsClustered(false);
        }
    }
}