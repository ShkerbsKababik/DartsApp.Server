using DartsApp.Server.Facades.AuthenticationService;
using System.Net.Http.Json;
using System.Net;
using DartsApp.Server.Client;

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
        public async Task CheckAnonymousAccessDenied()
        {
            // Act
            var responce = await _client.CheckAnonymousAsync();

            // Assert
            Assert.NotNull(responce);
        }
    }
}
