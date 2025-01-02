using DartsApp.Server.Models;
using DartsApp.Server.Facades;

namespace DartsApp.Server.Facades
{
    public class GameServiceFacade : IGameServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;
        public GameServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }
    }
}
