using DartsApp.Server.Facades;
using DartsApp.Server.Models.DataModels;
using DartsApp.Server.Models.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameServiceController : ControllerBase
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

        [HttpGet("ApplyScore")]
        public void ApplyScore(ApplyScoreInfo scoreInfo)
        {
            _gameServiceFacade.ApplyScore(scoreInfo);
        }

        [HttpPost("CreateGame")]
        public Guid CreateGame(GameCreationInfo creationInfo)
        {
            return _gameServiceFacade.CreateGame(creationInfo);
        }

        [HttpGet("ApplyScore1")]
        public void ApplyScore1(ApplyScoreInfo scoreInfo)
        {
            _gameServiceFacade.ApplyScore(scoreInfo);
        }
    }
}
