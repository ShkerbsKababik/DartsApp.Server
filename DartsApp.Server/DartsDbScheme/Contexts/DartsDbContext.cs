using Microsoft.EntityFrameworkCore;

namespace DartsDbScheme.Contexts
{
    public class DartsDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;

        public DartsDbContext(DbContextOptions<DartsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class Player
    { 
        public Guid Id { get; set; }

        public Guid AccessKey { get; set; }

        public string? Name { get; set; }
        public string? Password { get; set; }

        public List<Game> Games { get; set; } = new();
        public List<Score> Scores { get; set; } = new();
    }

    public class Game
    {
        public Guid Id { get; set; }

        public GameStatus GameStatus { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Guid OwnerId { get; set; }
        public Player? Owner { get; set; }

        public Guid LastPlayerId { get; set; }
        public Player? LastPlayer { get; set; }

        public List<Player> Players { get; set; } = new();
        public List<Score> Scores { get; set; } = new();
    }

    public class Score
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }
    }

    public enum GameStatus
    { 
        Open,
        Closed
    }
}

