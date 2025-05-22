using DartsApp.Server.Facades.AuthenticationService;
using System.Net.Http.Json;
using System.Net;

namespace DartsApp.Server.IntegrationTests
{
    public class SecurityServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SecurityServiceIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CheckAnonymousAccessDenied()
        {
            // Act
            var responce = await _client.GetAsync($"/AuthenticationService/CheckAnonymous");

            // Assert
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }
    }
}
