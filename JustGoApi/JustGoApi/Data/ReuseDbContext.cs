using JustGoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JustGoApi.Data
{
    public class ReuseDbContext : DbContext
    {
        public ReuseDbContext(DbContextOptions options) : base(options)
        {
        }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    }
}
