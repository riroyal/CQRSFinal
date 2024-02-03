using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().Property(x => x.Name).HasMaxLength(100);

            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Configurations.Add(new MovieConfiguration());
            //modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}
