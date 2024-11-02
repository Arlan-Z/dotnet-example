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
        protected override void OnModelCreating(ModelBuilder builder) // adding role to db
        {
            base.OnModelCreating(builder);
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