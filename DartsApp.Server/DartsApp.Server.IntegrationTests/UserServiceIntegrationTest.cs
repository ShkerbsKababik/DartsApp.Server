using System.Net.Http.Json;

using DartsApp.Server.Facades.UserService;
using DartsDbScheme.Contexts;

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
            // Test data
            string userName = "NewUser1";
            string userPassword = "123";

            // Act
            // Create user
            var userCreationInfo = new UserCreationInfo()
            {
                Name = userName,
                Password = userPassword
            };
            await _client.PostAsJsonAsync("/UserService/CreateUser", userCreationInfo);

            // Get user
            var getUserResponce = await _client.GetAsync($"/UserService/GetUserByName?userName={userName}");
            var accessedUser = await getUserResponce.Content.ReadFromJsonAsync<User>();

            // Delete user
            await _client.DeleteAsync($"/UserService/DeleteUser?userId={accessedUser?.Id}");

            // Assert
            // Check creation and access
            Assert.NotNull(accessedUser);
            Assert.Equal(userName, accessedUser?.Name);
            Assert.Equal(userPassword, accessedUser?.Password);

            // Check delition
            await Assert.ThrowsAsync<System.Text.Json.JsonException>(async () =>
            {
                var deleteUserResponce = await _client.GetAsync($"/UserService/GetUserByName?userName={userName}");
                var deletedUser = await deleteUserResponce.Content.ReadFromJsonAsync<User>();
            });
        }
    }
}