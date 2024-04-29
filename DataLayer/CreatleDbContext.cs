using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class CreatleDbContext : DbContext
    {
        public CreatleDbContext()
        {

        }

        public CreatleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-40HMVT5\\SQLEXPRESS;Database=CreatleDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().HasKey(x => new { x.Date, x.CategoryId, x.GameId });
            modelBuilder.Entity<HeroProfile>().HasKey(x => new { x.ValueId, x.GameId, x.HeroId, x.CategoryId });
            modelBuilder.Entity<Categories>().HasMany(c => c.CategoriesValues).WithOne(cv => cv.Category);
            modelBuilder.Entity<CategoriesValues>().HasMany(cv => cv.HeroProfiles).WithOne(hp => hp.Value).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CategoriesValues>().HasMany(cv => cv.Answers).WithOne(a => a.CategoryValue).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CategoriesValues> CategoriesValues { get; set; }
        public DbSet<HeroMetadata> HeroMetadata { get; set; }
        public DbSet<HeroProfile> HeroProfiles { get; set; }
    }
}