
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

        public Guid AddUser(string name)
        {
            return _userServiceFacade.AddUser(name);
        }

        public UserInfo GetUser(Guid id)
        {
            return _userServiceFacade.GetUser(id);
        }

        public List<UserInfo> GetUsers()
        {
            return _userServiceFacade.GetUsers();
        }
    }
}
