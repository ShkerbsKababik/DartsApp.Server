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
            // validate gameCreationInfo
            if (!_dartsDbContext.Users.Where(x => x.Id == gameCreationInfo.OwnerId).Any()) throw new Exception($"user not found {gameCreationInfo.OwnerId}");
            foreach (var playerId in gameCreationInfo.PlayersIds ?? throw new Exception("no players data fetched"))
            {
                if (!_dartsDbContext.Users.Where(x => x.Id == playerId).Any()) throw new Exception($"user not found {playerId}");
            }

            // create game 
            var owner = _dartsDbContext.Users.Where(x => x.Id == gameCreationInfo.OwnerId).FirstOrDefault();
            var players = _dartsDbContext.Users.Where(x => gameCreationInfo.PlayersIds.Contains(x.Id)).ToList();

            var random = new Random();
            var firstPlayer = players.OrderBy(x => random.Next()).FirstOrDefault();

            // create game and add users into it
            var game = new Game()
            { 
                Id = Guid.NewGuid(),
                GameStatus = GameStatus.Open,
                StartTime = DateTime.UtcNow,
                Owner = owner,
                LastUser = firstPlayer,
            };
            _dartsDbContext.Games.Add(game);
            game.Users.AddRange(players);
            
            // create scores and add them to game and db
            foreach (var player in players)
            {
                var score = new Score()
                {
                    Id = Guid.NewGuid(),
                    Game = game,
                    User = player,
                };
                _dartsDbContext.Scores.Add(score);
                game.Scores.Add(score);
            }

            _dartsDbContext.SaveChanges();
            return game.Id;
        }

        public void DeleteGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public GameInfo FetchGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void UpdateGame(GameUpdateInfo gameUpdateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
