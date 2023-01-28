using Agency.Models;
using Microsoft.EntityFrameworkCore;

namespace Agency.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
