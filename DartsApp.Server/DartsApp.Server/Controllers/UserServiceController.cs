using Microsoft.AspNetCore.Mvc;
using DartsApp.Server.Facades.UserService;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserServiceController : ControllerBase, IUserServiceFacade
    {
        private readonly IUserServiceFacade _userServiceFacade;

        public UserServiceController(IUserServiceFacade userServiceFacade)
        {
            _userServiceFacade = userServiceFacade;
        }

        [HttpPost]
        public Guid AddUser(string name)
        {
            return _userServiceFacade.AddUser(name);
        }

        [HttpGet]
        public UserInfo GetUser(Guid id)
        {
            return _userServiceFacade.GetUser(id);
        }

        [HttpGet]
        public List<UserInfo> GetUsers()
        {
            return _userServiceFacade.GetUsers();
        }
    }
}
