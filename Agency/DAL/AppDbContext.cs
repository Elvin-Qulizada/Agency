using Agency.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agency.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}
