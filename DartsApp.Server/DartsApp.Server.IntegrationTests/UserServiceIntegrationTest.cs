using DartsApp.Server.Facades.UserService;
using DartsDbScheme.Contexts;
using System.Net.Http.Json;

namespace DartsApp.Server.IntegrationTests
{
    public class UserServiceIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserServiceIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task UserServiceCrudCycle()
        {
            // Arrange
            string userName = "NewUser1";
            string userPassword = "123";

            // Act
            await _client.PostAsJsonAsync("/UserService/CreateUser", "");

            // Get user
            var responce = await _client.GetAsync($"/UserService/GetUserByName?userName={userName}");

            // Assert
            Assert.NotNull(responce);
        }
    }
}