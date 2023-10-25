using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi
{
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Car> Cars { get; set; }
    }
}
