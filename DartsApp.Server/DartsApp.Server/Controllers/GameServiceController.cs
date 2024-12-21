using DartsApp.Server.Facades;
using DartsApp.Server.Models.DataModels;
using DartsApp.Server.Models.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameServiceController : ControllerBase, IGameServiceFacade
    {
        private readonly ILogger<GameServiceController> _logger;
        private readonly IGameServiceFacade _gameServiceFacade;

        public GameServiceController(
            ILogger<GameServiceController> logger,
            IGameServiceFacade gameServiceFacade)
        {
            _logger = logger;
            _gameServiceFacade = gameServiceFacade;
        }

        [HttpPost]
        public void ApplyScore(ApplyScoreInfo scoreInfo)
        {
            _gameServiceFacade.ApplyScore(scoreInfo);
        }

        [HttpPost]
        public Guid CreateGame(GameCreationInfo creationInfo)
        {
            return _gameServiceFacade.CreateGame(creationInfo);
        }

        [HttpGet("{gameId}")]
        public Game GetGameInfo(Guid gameId)
        {
            return _gameServiceFacade.GetGameInfo(gameId);
        }
    }
}
