using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}
