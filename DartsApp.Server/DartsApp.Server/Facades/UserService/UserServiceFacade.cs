
using DartsDbScheme.Contexts;

namespace DartsApp.Server.Facades.UserService
{
    public class UserServiceFacade : IUserServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;

        public UserServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }

        public Guid AddUser(string name)
            => AddUserAsync(name).Result;
        public async Task<Guid> AddUserAsync(string name)
        {
            var user = _dartsDbContext.Users.FirstOrDefault(u => u.Name == name);
            if (user != null) throw new Exception("user already exists");

            user = new User() 
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _dartsDbContext.AddAsync(user);
            await _dartsDbContext.SaveChangesAsync();

            return user.Id;
        }

        public UserInfo GetUser(Guid id)
        {
            var user = _dartsDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) throw new Exception("invalid data");

            return UserInfo.FromDomain(user);
        }

        public List<UserInfo> GetUsers()
        {
            return _dartsDbContext.Users.Select(u => UserInfo.FromDomain(u)).ToList();
        }
    }
}
