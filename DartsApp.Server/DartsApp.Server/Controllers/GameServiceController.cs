using DartsApp.Server.Facades;
using Microsoft.AspNetCore.Mvc;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameServiceController : ControllerBase, IGameServiceFacade
    {
        private readonly IGameServiceFacade _gameServiceFacade;

        public GameServiceController(
            IGameServiceFacade gameServiceFacade)
        {
            _gameServiceFacade = gameServiceFacade;
        }

        //[HttpPost]
        //public Game ApplyScore(ApplyScoreInfo scoreInfo)
        //{
        //    return _gameServiceFacade.ApplyScore(scoreInfo);
        //}

        //[HttpGet("{gameId}")]
        //public Game GetGameInfo(Guid gameId)
        //{
        //    return _gameServiceFacade.GetGameInfo(gameId);
        //}
    }
}
