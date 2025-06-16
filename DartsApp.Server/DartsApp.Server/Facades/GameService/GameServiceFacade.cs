using Microsoft.EntityFrameworkCore;
using DartsDbScheme.Contexts;

namespace DartsApp.Server.Facades.GameService
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
            var players = _dartsDbContext.Users.Where(u => gameCreationInfo.PlayerIds.Contains(u.Id)).ToList();
            if (owner == null && players.Count() != gameCreationInfo.PlayerIds.Length) throw new Exception("invalid data");

            var game = new Game()
            {
                Id = Guid.NewGuid(),
                OwnerId = owner?.Id ?? Guid.Empty,
                CurrentPlayerId = owner?.Id ?? Guid.Empty,
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
            var game = _dartsDbContext.Games
                .Include(g => g.Players)
                .Include(g => g.Scores)
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null) throw new Exception("invalid data");
            var gameInfo = new GameInfo() 
            { 
                Id = Guid.NewGuid(),
                OwnerId= game.OwnerId,
                CurrentPlayerId = game.CurrentPlayerId,
            };

            gameInfo.Players = game.Players.Select(PlayerInfo.FromDomain).ToArray();
            gameInfo.Scores = game.Scores.Select(ScoreInfo.FromDomain).ToArray();

            return gameInfo ?? throw new Exception("internal exception");
        }

        public void UpdateScore(UpdateScoreInfo gameScoreInfo)
            => UpdateScoreAsync(gameScoreInfo).Wait();

        public async Task UpdateScoreAsync(UpdateScoreInfo updateScoreInfo)
        {
            var score = _dartsDbContext.Scores.FirstOrDefault(s => s.Id == updateScoreInfo.ScoreId);
            if (score == null) throw new Exception("invalid data");

            score.Value = updateScoreInfo.Value;
            await _dartsDbContext.SaveChangesAsync();
        }
    }
}
