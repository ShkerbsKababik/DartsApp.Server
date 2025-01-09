namespace DartsApp.Server.Facades.UserService
{
    public interface IUserServiceFacade
    {
        public void CreateUser(UserCreationInfo userCreationInfo);
        public void UpdateUser(UserUpdateInfo userUpdateInfo);
        public void DeleteUser(Guid userId);

        public User GetUser(Guid userId);
        public List<User> GetUsers();
    }
    public class UserUpdateInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
    public class UserCreationInfo
    { 
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
