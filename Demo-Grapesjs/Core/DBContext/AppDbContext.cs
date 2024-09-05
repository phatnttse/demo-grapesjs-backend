using Demo_Grapesjs.Entities;
using Microsoft.EntityFrameworkCore;


namespace Demo_Grapesjs.Core.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NameCardTemplate> NameCardTemplates { get; set; }
        public DbSet<UserNameCard> UserNameCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ giữa User và UserNameCard
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserNameCards)
                .WithOne(unc => unc.User)
                .HasForeignKey(unc => unc.UserId); // Khóa ngoại trong UserNameCard

            // Cấu hình quan hệ giữa NameCardTemplate và UserNameCard
            modelBuilder.Entity<NameCardTemplate>()
                .HasMany(nct => nct.UserNameCards)
                .WithOne(unc => unc.NameCardTemplate)
                .HasForeignKey(unc => unc.NameCardTemplateId); // Khóa ngoại trong UserNameCard
        }
    }
    }
