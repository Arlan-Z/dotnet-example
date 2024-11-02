using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser> // Saying that, AppUser - User Object
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) // adding role to db
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser) // each portfolio has one owner - appuser
                .WithMany(u => u.Portfolios) // user can have multiple portfolios
                .HasForeignKey(u => u.AppUserId); // AppUserId - Foreign key

            
            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(u => u.StockId);

            List<IdentityRole>  roles = new List<IdentityRole>{
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"    
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"    
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}