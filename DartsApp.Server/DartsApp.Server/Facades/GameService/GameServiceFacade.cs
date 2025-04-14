
using Microsoft.AspNetCore.Http.HttpResults;

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
            => CreateGameAsync(gameCreationInfo).Result;

        public async Task<Guid> CreateGameAsync(GameCreationInfo gameCreationInfo)
        {
            var owner = _dartsDbContext.Users.FirstOrDefault(u => u.Id == gameCreationInfo.OwnerId);
            var players = _dartsDbContext.Users.Where(u => gameCreationInfo.PlayerIds.Contains(u.Id));
            if (owner == null && players.Count() != gameCreationInfo.PlayerIds.Count) throw new Exception("invalid data");

            var game = new Game()
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                CurrentPlayer = owner,
                Players = new List<User>(),
                Scores = new List<Score>()
            };

            foreach (var player in players)
            {
                game.Players.Add(player);
                game.Scores.Add(new Score() 
                {
                    Id = Guid.NewGuid(),
                    Game = game,
                    Owner = player,
                    Value = 301
                });
            }

            await _dartsDbContext.Games.AddAsync(game);
            await _dartsDbContext.SaveChangesAsync();

            return game.Id;
        }

        public GameInfo GetGameInfo(Guid gameId)
        {
            var game = _dartsDbContext.Games.FirstOrDefault(g => g.Id == gameId);
            if (game == null) throw new Exception("invalid data");

            return GameInfo.FromDomain(game);
        }

        public void UpdateScore(GameScoreInfo gameScoreInfo)
            => UpdateScoreAsync(gameScoreInfo).Wait();

        public async Task UpdateScoreAsync(GameScoreInfo gameScoreInfo)
        {
            var score = _dartsDbContext.Scores.FirstOrDefault(s => s.Id == gameScoreInfo.ScoreId);
            if (score == null) throw new Exception("invalid data");

            score.Value = gameScoreInfo.Value;
            await _dartsDbContext.SaveChangesAsync();
        }
    }
}
