using JustGoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JustGoApi.Data
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions options) : base(options)
        {
        }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    }
}
