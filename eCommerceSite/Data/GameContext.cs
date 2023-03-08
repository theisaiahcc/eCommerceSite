using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) 
        {

        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
