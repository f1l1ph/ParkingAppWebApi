using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasIndex(u => u.PlateNumber)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;
    }
}
