using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(flight => flight.Id);
            modelBuilder.Entity<Flight>().OwnsMany(flight => flight.BookingList);
            base.OnModelCreating(modelBuilder);
        }

    }
}