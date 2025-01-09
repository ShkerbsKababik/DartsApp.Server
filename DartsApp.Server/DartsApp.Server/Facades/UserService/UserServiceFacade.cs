
namespace DartsApp.Server.Facades.UserService
{
    public class UserServiceFacade : IUserServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;

        public UserServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }

        public void CreateUser(UserCreationInfo userCreationInfo)
            => CreateUserAsync(userCreationInfo).Wait();
        public async Task CreateUserAsync(UserCreationInfo userCreationInfo)
        {
            if (_dartsDbContext.Users.Where(x => x.Name == userCreationInfo.Name).Any())
            {
                throw new Exception($"user {userCreationInfo.Name} already exist");
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                AccessKey = Guid.NewGuid(),
                Name = userCreationInfo.Name,
                Password = userCreationInfo.Password,
            };

            await _dartsDbContext.Users.AddAsync(user);
            await _dartsDbContext.SaveChangesAsync();
        }

        public void UpdateUser(UserUpdateInfo userUpdateInfo)
            => UpdateUserAsync(userUpdateInfo).Wait();
        public async Task UpdateUserAsync(UserUpdateInfo userUpdateInfo)
        {
            var user = _dartsDbContext.Users.Where(x => x.Id == userUpdateInfo.Id).FirstOrDefault();
            if (user != null)
            {
                user.Name = userUpdateInfo?.Name;
                user.Password = userUpdateInfo?.Password;

                await _dartsDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"user {userUpdateInfo.Id} does not exist");
            }
        }

        public void DeleteUser(Guid userId)
            => DeleteUserAsync(userId).Wait();
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = _dartsDbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
                _dartsDbContext.Users.Remove(user);
                await _dartsDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"user {userId} does not exist");
            }
        }

        public User GetUser(Guid userId)
        {
            return _dartsDbContext.Users.Where(x => x.Id == userId).FirstOrDefault()
                ?? throw new Exception($"user {userId} does not exist");
        }

        public List<User> GetUsers()
        {
            return _dartsDbContext.Users.ToList();
        }
    }
}
