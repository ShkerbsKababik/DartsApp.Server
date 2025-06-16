using DartsApp.Server.Facades.GameService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public Guid CreateGame(GameCreationInfo gameCreationInfo)
        {
            return _gameServiceFacade.CreateGame(gameCreationInfo);
        }

        [HttpGet]
        public GameInfo GetGameInfo(Guid gameId)
        {
            return _gameServiceFacade.GetGameInfo(gameId);
        }

        [HttpPost]
        public void UpdateScore(UpdateScoreInfo gameScoreInfo)
        {
            _gameServiceFacade.UpdateScore(gameScoreInfo);
        }
    }
}
