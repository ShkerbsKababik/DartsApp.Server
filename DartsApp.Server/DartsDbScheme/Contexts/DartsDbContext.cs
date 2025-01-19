using Microsoft.EntityFrameworkCore;

namespace DartsDbScheme.Contexts
{
    public class DartsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;

        public DartsDbContext(DbContextOptions<DartsDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.LastUser) 
                .WithMany()
                .HasForeignKey("LastUserId");

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Owner)
                .WithMany()
                .HasForeignKey("OwnerId");
        }
    }

    public class User
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
        public User? Owner { get; set; }

        public Guid LastUserId { get; set; }
        public User? LastUser { get; set; }

        public List<User> Users { get; set; } = new();
        public List<Score> Scores { get; set; } = new();
    }

    public class Score
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }

    public enum GameStatus
    { 
        Open,
        Closed
    }
}

