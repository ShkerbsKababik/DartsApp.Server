
namespace DartsApp.Server.Facades
{
    public class GameServiceFacade : IGameServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;
        public GameServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }

        public Guid CreateGame(GameCreationInfo gameCreationInfo)
        {
            var owner = _dartsDbContext.Users.Where(u => u.Id == gameCreationInfo.OwnerId).FirstOrDefault();
            if (owner != null)
            {
                var game = new Game()
                {
                    Owner = owner,
                    CurrentPlayer = owner,
                };

                foreach (var playerId in gameCreationInfo.PlayerIds)
                {
                    var player = _dartsDbContext.Users.Where(u => u.Id == playerId).FirstOrDefault();
                    if (player != null)
                    { 
                        game.Players.Add(player);
                        game.Scores.Add(new Score() 
                        { 
                            Game = game,
                            Owner = player,
                            Value = 301
                        });
                    }
                }


            }
        }

        public GameInfo GetGameInfo(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void UpdateScore(GameScoreInfo gameScoreInfo)
        {
            throw new NotImplementedException();
        }
    }
}
