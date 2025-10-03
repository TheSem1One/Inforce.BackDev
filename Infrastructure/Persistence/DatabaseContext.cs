using System.Reflection;
using Domain.Entities;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ShortUrl> ShortUrl { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
