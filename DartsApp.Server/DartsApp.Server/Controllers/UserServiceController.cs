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
        public void CreateUser(UserCreationInfo userCreationInfo)
            => _userServiceFacade.CreateUser(userCreationInfo);

        [HttpPatch]
        public void UpdateUser(UserUpdateInfo userUpdateInfo)
            => _userServiceFacade.UpdateUser(userUpdateInfo);

        [HttpDelete]
        public void DeleteUser(Guid userId)
            => _userServiceFacade.DeleteUser(userId);

        [HttpGet]
        public User GetUser(Guid userId)
            => _userServiceFacade.GetUser(userId);

        [HttpGet]
        public User GetUserByName(string userName)
            => _userServiceFacade.GetUserByName(userName);

        [HttpGet]
        public List<User> GetUsers()
            => _userServiceFacade.GetUsers();
    }
}
