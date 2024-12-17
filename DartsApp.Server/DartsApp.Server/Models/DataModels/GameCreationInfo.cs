namespace DartsApp.Server.Models.DataModels
{
    public class GameCreationInfo
    {
        public Guid OwnerId { get; set; }
        public List<Guid> Players { get; set; }
    }
}
