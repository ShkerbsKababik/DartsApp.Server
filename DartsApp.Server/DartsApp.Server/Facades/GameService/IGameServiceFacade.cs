﻿namespace DartsApp.Server.Facades
{
    public interface IGameServiceFacade
    {
        public Guid CreateGame(GameCreationInfo gameCreationInfo);
        public GameInfo GetGameInfo(Guid gameId);
        public void UpdateScore(GameScoreInfo gameScoreInfo);
    }

    public class GameCreationInfo
    {
        public List<Guid> PlayerIds { get; set; }
        public Guid OwnerId { get; set; }
    }

    public class GameInfo
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public User CurrentPlayer { get; set; }
        public ICollection<User> Players { get; set; }
        public ICollection<Score> Scores { get; set; }

        public static GameInfo FromDomain(Game game)
        {
            var gameInfo = new GameInfo();

            gameInfo.Id = game.Id;
            gameInfo.Owner = game.Owner;
            gameInfo.CurrentPlayer = game.CurrentPlayer;
            gameInfo.Players = game.Players;
            gameInfo.Scores = game.Scores;

            return new GameInfo() 
            { 
                Id = game.Id,
                Owner = game.Owner,
                CurrentPlayer = game.CurrentPlayer,
                Players = game.Players,
                Scores = game.Scores,
            };
        }
    }

    public class GameScoreInfo
    {
        public Guid ScoreId { get; set; }
        public int Value { get; set; }
    }
}
