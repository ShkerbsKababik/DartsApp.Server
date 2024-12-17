using Microsoft.EntityFrameworkCore;

namespace DartsApp.Server.Models.DbModel
{
    public class DartsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GameStage> GameStages { get; set; } = null!;
        public DbSet<GameScore> GameScores { get; set; } = null!;
        public DartsDbContext(DbContextOptions<DartsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
