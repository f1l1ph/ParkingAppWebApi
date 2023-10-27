using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasIndex(u => u.PlateNumber)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Car> Cars { get; set; }
    }
}
