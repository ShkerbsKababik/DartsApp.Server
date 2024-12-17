namespace DartsApp.Server.Models.DbModel
{
    public class GameStage
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        public int FirstRun { get; set; }
        public int SecondRun { get; set; }
        public int ThirdRun { get; set; }
    }
}
