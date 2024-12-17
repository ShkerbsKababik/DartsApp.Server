using DartsApp.Server.Models.DataModels;
using DartsApp.Server.Models.DbModel;

namespace DartsApp.Server.Facades
{
    public interface IGameServiceFacade
    {
        public Guid CreateGame(GameCreationInfo creationInfo);
        public void ApplyScore(ApplyScoreInfo scoreInfo);
        public Game GetGameInfo(Guid GameId);
    }
}
