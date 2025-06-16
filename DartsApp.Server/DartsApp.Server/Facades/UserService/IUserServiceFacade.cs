using DartsDbScheme.Contexts;

namespace DartsApp.Server.Facades.UserService
{
    public interface IUserServiceFacade
    {
        public List<UserInfo> GetUsers();
        public UserInfo GetUser(Guid id);
        public Guid AddUser(string name);
    }

    public class UserInfo()
    { 
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public static UserInfo FromDomain(User user)
        {
            return new UserInfo() 
            { 
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}
