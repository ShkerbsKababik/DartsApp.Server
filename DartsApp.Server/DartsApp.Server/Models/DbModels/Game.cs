namespace DartsApp.Server.Models.DbModel
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public Guid CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Guid[] Players { get; set; }
        public Guid[] Scores { get; set; }
    }
    public enum GameStatus
    {
        InProcess,
        Canceled,
        Ended
    }
}
