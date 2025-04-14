
namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameServiceController : ControllerBase, IGameServiceFacade
    {
        private readonly IGameServiceFacade _gameServiceFacade;

        public GameServiceController(IGameServiceFacade gameServiceFacade)
        {
            _gameServiceFacade = gameServiceFacade;
        }

        public Guid CreateGame(GameCreationInfo gameCreationInfo)
        {
            return _gameServiceFacade.CreateGame(gameCreationInfo);
        }

        public GameInfo GetGameInfo(Guid gameId)
        {
            return _gameServiceFacade.GetGameInfo(gameId);
        }

        public void UpdateScore(GameScoreInfo gameScoreInfo)
        {
            _gameServiceFacade.UpdateScore(gameScoreInfo);
        }
    }
}
