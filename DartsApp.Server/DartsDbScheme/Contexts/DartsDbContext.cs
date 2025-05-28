using Microsoft.EntityFrameworkCore;

namespace DartsDbScheme.Contexts
{
    public class DartsDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Score> Scores { get; set; }
        public DartsDbContext(DbContextOptions<DartsDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Game mapping
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Owner)
                .WithMany()
                .HasForeignKey(g => g.OwnerId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.CurrentPlayer)
                .WithMany()
                .HasForeignKey(g => g.CurrentPlayerId);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Players)
                .WithMany(p => p.Games);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Scores)
                .WithOne(s => s.Game);

            // User mapping
            modelBuilder.Entity<User>()
                .HasMany(u => u.Scores)
                .WithOne(s => s.Owner);
        }
    }

    public class Game
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }
        public Guid CurrentPlayerId { get; set; }
        public User? CurrentPlayer { get; set; }
        public ICollection<User> Players { get; set; } = new List<User>();
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }

    public class Score
    {
        public Guid Id { get; set; }
        public Game? Game { get; set; }
        public User? Owner { get; set; }
        public int Value { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}

