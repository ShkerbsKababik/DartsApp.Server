using DartsApp.Server.Facades.UserService;
using DartsDbScheme.Contexts;

namespace DartsApp.Server.Facades.GameService
{
    public interface IGameServiceFacade
    {
        public Guid CreateGame(GameCreationInfo gameCreationInfo);
        public GameInfo GetGameInfo(Guid gameId);
        public void UpdateScore(UpdateScoreInfo gameScoreInfo);
    }

    public class GameInfo
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid CurrentPlayerId { get; set; }
        public PlayerInfo[]? Players { get; set; }
        public ScoreInfo[]? Scores { get; set; }
    }

    public class PlayerInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public static PlayerInfo FromDomain(User user)
        {
            return new PlayerInfo()
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }

    public class ScoreInfo
    {
        public Guid Owner { get; set; }
        public int Value { get; set; }

        public static ScoreInfo FromDomain(Score score)
        {
            return new ScoreInfo()
            {
                Owner = score.Owner.Id,
                Value = score.Value
            };
        }
    }

    public class GameCreationInfo
    {
        public Guid[]? PlayerIds { get; set; }
        public Guid OwnerId { get; set; }
    }

    public class UpdateScoreInfo
    {
        public Guid ScoreId { get; set; }
        public int Value { get; set; }
    }
}
