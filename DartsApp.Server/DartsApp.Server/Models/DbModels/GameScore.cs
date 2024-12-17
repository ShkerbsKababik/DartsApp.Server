namespace DartsApp.Server.Models.DbModel
{
    public class GameScore
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PalyerId { get; set; }
        public int Score { get; set; }
    }
}
