using DartsApp.Server.Client;

namespace DartsApp.Server.IntegrationTests
{
    public class GameServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly Client.Client _client;

        public GameServiceIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            var client = factory.CreateClient();
            var baseUrl = client.BaseAddress?.ToString();

            _client = new Client.Client(baseUrl, client);
        }

        [Fact]
        public async Task GameCreationTest()
        {
            // Arrange
            var userName = $"new user {Guid.NewGuid()}";

            // Act
            var createdUserId = await _client.AddUserAsync(userName);
            var createdUser = await _client.GetUserAsync(createdUserId);

            var createdGameId = await _client.CreateGameAsync(new GameCreationInfo() 
            { 
                OwnerId = createdUserId,
                PlayerIds = new List<Guid>() { createdUserId }
            });
            var createdGameInfo = await _client.GetGameInfoAsync(createdGameId);

            // Assert
            Assert.NotNull(createdGameInfo);
            Assert.Equal(createdGameInfo.OwnerId, createdUser.Id);
        }
    }
}
