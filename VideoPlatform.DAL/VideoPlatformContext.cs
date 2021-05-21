using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoPlatform.DAL.DataModels;
using VideoPlatform.DAL.Infrastructure.Configurations;
using VideoPlatform.DAL.Infrastructure.Extensions;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL
{
    /// <summary>
    /// VideoPlatform Context
    /// </summary>
    public class VideoPlatformContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public VideoPlatformContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Experiment> Experiments { get; set; }

        public virtual DbSet<Favorite> Favorites { get; set; }

        public virtual DbSet<Media> Media { get; set; }

        public virtual DbSet<MediaTag> MediaTags { get; set; }

        public virtual DbSet<Partner> Partners { get; set; }

        public virtual DbSet<PartnerMedia> PartnerMedia { get; set; }

        public virtual DbSet<PartnerTypes> PartnerTypes { get; set; }

        public virtual DbSet<Playlist> Playlists { get; set; }

        public virtual DbSet<PlaylistMedia> PlaylistMedia { get; set; }

        public virtual DbSet<Series> Series { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<SubscriptionSeries> SubscriptionSeries { get; set; }

        public virtual DbSet<SubscriptionTopic> SubscriptionTopics { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Tool> Tools { get; set; }

        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<AppUser> AppUsers { get; set; }

        public virtual DbSet<AppRole> AppUserRoles { get; set; }

        public DbSet<UserRoleDataModel> UserRoleData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>().HasOne(x => x.UploadUser).WithMany(x => x.UploadMedia)
                .HasForeignKey("UploadUserId").IsRequired();

            modelBuilder.Entity<Media>().HasOne(x => x.PublishUser).WithMany(x => x.PublishMedia)
                .HasForeignKey("PublishUserId").IsRequired(false);

            modelBuilder.ApplyConfiguration(new ExperimentConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new MediaTagConfiguration());
            modelBuilder.ApplyConfiguration(new PartnerConfiguration());
            modelBuilder.ApplyConfiguration(new PartnerMediaConfiguration());
            modelBuilder.ApplyConfiguration(new PartnerTypesConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistMediaConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesConfiguration());
            modelBuilder.ApplyConfiguration(new SettingConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionTopicConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ToolConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

            modelBuilder.Entity<UserRoleDataModel>().HasNoKey().ToView("UserRolesView");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
    }
}