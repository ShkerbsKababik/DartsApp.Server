namespace DartsApp.Server.IntegrationTests
{
    public class UserServiceIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly Client.Client _client;

        public UserServiceIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            var client = factory.CreateClient();
            var baseUrl = client.BaseAddress?.ToString();

            _client = new Client.Client(baseUrl, client);
        }

        [Fact]
        public async Task AddUserTest()
        {
            // Arrange
            var userName = $"new user {Guid.NewGuid()}";

            // Act
            var createdUserId = await _client.AddUserAsync(userName);
            var createdUser = await _client.GetUserAsync(createdUserId);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(createdUserId, createdUser.Id);
            Assert.Equal(userName, createdUser.Name);
        }

        [Fact]
        public async Task GetUsersTest()
        {
            // Arrange
            var userCount = 10;
            var userNamesToAdd = new List<string>();
            for (int i = 0; i < userCount; i++) 
            { 
                userNamesToAdd.Add($"new user {i}");
            }

            // Act
            foreach (var userName in userNamesToAdd)
            {
                await _client.AddUserAsync(userName);
            }

            var users = await _client.GetUsersAsync();

            // Assert
            var createdUserNames = users.Select(x => x.Name).ToList();

            Assert.True(users.Count >= userCount);
            foreach (var userName in userNamesToAdd)
            {
                Assert.Contains(userName, createdUserNames);
            }
        }
    }
}