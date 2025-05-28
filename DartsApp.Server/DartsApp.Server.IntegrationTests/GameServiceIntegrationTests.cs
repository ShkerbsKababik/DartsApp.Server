using DartsApp.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                PlayerIds = { createdUserId } 
            });

            var createdGameInfo = await _client.GetGameInfoAsync(createdUserId);

            // Assert
            Assert.NotNull(createdGameInfo);
            Assert.Equal(createdGameInfo.Owner.Name, userName);
        }
    }
}
