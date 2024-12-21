using DartsApp.Server.Models.DataModels;
using DartsApp.Server.Models.DbModel;

namespace DartsApp.Server.Facades
{
    public class GameServiceFacade : IGameServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;
        public GameServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }

        public void ApplyScore(ApplyScoreInfo scoreInfo)
            => ApplyScoreAsync(scoreInfo);

        public async void ApplyScoreAsync(ApplyScoreInfo scoreInfo)
        {
            GameScore gameScore = _dartsDbContext.GameScores.Where(x => x.PalyerId == scoreInfo.PlayerId && x.GameId == scoreInfo.GameId).First();
            if (gameScore == null)
            {
                throw new Exception($"game {scoreInfo.GameId} not found");
            }

            gameScore.Score -= scoreInfo.FirstRun + scoreInfo.SecondRun + scoreInfo.ThirdRun;
            _dartsDbContext.GameScores.Update(gameScore);

            GameStage gameStage = new GameStage()
            { 
                Id = Guid.NewGuid(),
                GameId = scoreInfo.GameId,
                PlayerId = scoreInfo.PlayerId,

                FirstRun = scoreInfo.FirstRun,
                SecondRun = scoreInfo.SecondRun,
                ThirdRun = scoreInfo.ThirdRun,
            };
            await _dartsDbContext.GameStages.AddAsync(gameStage);

            await _dartsDbContext.SaveChangesAsync();
        }

        public Guid CreateGame(GameCreationInfo creationInfo)
            => CreateGameAsync(creationInfo).Result;

        public async Task<Guid> CreateGameAsync(GameCreationInfo creationInfo)
        {
            Random random = new Random();

            Guid gameId = Guid.NewGuid();

            List<Guid>Scores = new List<Guid>();
            foreach (var playerId in creationInfo.Players)
            { 
                Guid gameScoreId = Guid.NewGuid();
                GameScore gameScore = new GameScore() 
                {
                    Id = gameScoreId,
                    GameId = gameId,
                    PalyerId = playerId,
                    Score = 301
                };

                Scores.Add(gameScoreId);
                await _dartsDbContext.GameScores.AddAsync(gameScore);
            }

            Game game = new Game()
            {
                 Id = gameId,
                 Owner = creationInfo.OwnerId,
                 CurrentPlayer = creationInfo.Players.OrderBy(x => random.Next()).First(),
                 Status = GameStatus.InProcess,
                 CreationTime = DateTime.UtcNow,

                 Players = creationInfo.Players.OrderBy(x => random.Next()).ToArray(),
                 Scores = Scores.ToArray(),
            };
            await _dartsDbContext.Games.AddAsync(game);

            await _dartsDbContext.SaveChangesAsync();
            return game.Id;
        }

        public Game GetGameInfo(Guid GameId)
        {
            return _dartsDbContext.Games.Where(x => x.Id == GameId).FirstOrDefault();
        }
    }
}
