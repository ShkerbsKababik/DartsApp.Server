using DartsApp.Server.Facades.AuthenticationService;
using System.Net.Http.Json;
using System.Net;
using DartsApp.Server.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DartsApp.Server.IntegrationTests
{
    public class SecurityServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly Client.Client _client;
        public SecurityServiceIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            var client = factory.CreateClient();
            var baseUrl = client.BaseAddress?.ToString();

            _client = new Client.Client(baseUrl, client);
        }

        [Fact]
        public async Task AnonymousAccessTest()
        {
            // Act
            var responce = await _client.CheckAnonymousAsync();

            // Assert
            Assert.NotNull(responce);
            Assert.Equal("allowed anonymous", responce);
        }

        [Fact]
        public async Task NonAuthorizeAccessTest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(_client.CheckAuthorizeAsync);
            Assert.Contains("405", exception.Message);
        }

        [Fact]
        public async Task AuthorizeAccessTest()
        {
            // Act
            await _client.LoginAsync(new Client.AuthenticationInfo()
            {
                Login = "admin",
                Password = "admin"
            });
            var responce = await _client.CheckAuthorizeAsync();

            // Assert
            Assert.NotNull(responce);
            Assert.Equal("allowed authorize", responce);
        }
    }
}
