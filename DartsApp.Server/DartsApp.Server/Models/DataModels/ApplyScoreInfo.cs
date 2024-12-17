namespace DartsApp.Server.Models.DataModels
{
    public class ApplyScoreInfo
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        public int FirstRun { get; set; }
        public int SecondRun { get; set; }
        public int ThirdRun { get; set; }
    }
}
