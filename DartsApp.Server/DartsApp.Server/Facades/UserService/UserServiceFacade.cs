namespace DartsApp.Server.Facades.UserService
{
    public class UserServiceFacade : IUserServiceFacade
    {
        private readonly DartsDbContext _dartsDbContext;

        public UserServiceFacade(DartsDbContext dartsDbContext)
        {
            _dartsDbContext = dartsDbContext;
        }

        public void AddUser(UserCreationInfo userCreationInfo)
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

            _dartsDbContext.Users.Add(user);
            _dartsDbContext.SaveChanges();
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
