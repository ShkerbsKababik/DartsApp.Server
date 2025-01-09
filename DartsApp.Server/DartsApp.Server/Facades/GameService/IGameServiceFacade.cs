namespace DartsApp.Server.Facades
{
    public interface IGameServiceFacade
    {
        public Guid CreateGame(GameCreationInfo gameCreationInfo);
        public void UpdateGame(GameUpdateInfo gameUpdateInfo);
        public void DeleteGame(Guid gameId);
        public GameInfo FetchGame(Guid gameId);
    }
    public class GameCreationInfo
    { 
        public Guid OwnerId { get; set; }
        public List<Guid>? PlayersIds { get; set; }
    }
    public class GameUpdateInfo
    { 
        
    }
    public class GameInfo
    {
        
    }
}
