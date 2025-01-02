using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DartsApp.Server.Models
{
    public class DartsDbContext : DbContext
    {
        // public DbSet<User> Users { get; set; } = null!;
        public DartsDbContext(DbContextOptions<DartsDbContext> options) : base(options)
        {
            Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
    } 
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Guid? OwnerId { get; set; }
        public Guid? LastPalyerId { get; set; }

        public GameStatus Status { get; set; }
    }
    public enum GameStatus
    { 
        Open,
        Closed
    }
}
