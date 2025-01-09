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
        public void AddUser(UserCreationInfo userCreationInfo)
        {
            _userServiceFacade.AddUser(userCreationInfo);
        }

        [HttpGet]
        public User GetUser(Guid userId)
        {
            return _userServiceFacade.GetUser(userId);
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return _userServiceFacade.GetUsers();
        }
    }
}
