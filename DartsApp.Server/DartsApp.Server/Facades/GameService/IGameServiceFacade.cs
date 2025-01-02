using DartsApp.Server.Models;

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
        public List<Guid>? Players { get; set; }
    }
    public class GameUpdateInfo
    { 
        
    }
    public class GameInfo
    { 
        
    }
    public class GameServiceFactory
    {
        public virtual Game CreateGame(GameCreationInfo gameCreationInfo)
        { 
            Game game = new Game() 
            { 
                Id = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                OwnerId = gameCreationInfo.OwnerId,
                Status = GameStatus.Open
            };

            return game;
        }
    }
}
