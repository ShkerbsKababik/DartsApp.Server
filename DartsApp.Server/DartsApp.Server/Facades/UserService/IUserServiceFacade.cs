namespace DartsApp.Server.Facades.UserService
{
    public interface IUserServiceFacade
    {
        public void AddUser(UserCreationInfo userCreationInfo);
        public List<User> GetUsers();
        public User GetUser(Guid userId);
    }
    public class UserCreationInfo
    { 
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
