using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Configurations
{
    internal class SubscriptionTopicConfiguration : IEntityTypeConfiguration<SubscriptionTopic>
    {
        public void Configure(EntityTypeBuilder<SubscriptionTopic> builder)
        {
            builder.ToTable("SubscriptionTopics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.TopicId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasIndex(x => new {x.UserId, x.TopicId}).IsUnique().ForSqlServerIsClustered(false);
        }
    }
}