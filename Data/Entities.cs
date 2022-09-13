using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();
    }
}