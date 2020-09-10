using Microsoft.EntityFrameworkCore;

namespace leashApi.Models
{
    public class ParkContext : DbContext
    {
        public ParkContext(DbContextOptions<ParkContext> options)
            : base(options)
        {
        }

        public DbSet<ParkItem> ParkItems { get; set; }
    }
}