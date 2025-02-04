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
        public void DeleteGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public GameInfo FetchGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void UpdateGame(GameUpdateInfo gameUpdateInfo)
        {
            throw new NotImplementedException();
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
