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

        [Fact]
        public async Task CheckAuthorizeAccessDenied()
        {
            // Act
            var responce = await _client.GetAsync($"/AuthenticationService/CheckAuthorize");

            // Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, responce.StatusCode);
        }

        [Fact]
        public async Task CheckAuthorizeAccessGranted()
        {
            // Arrange
            var authenticationInfo = new AuthenticationInfo()
            {
                Login = "admin",
                Password = "admin"
            };

            // Act
            await _client.PostAsJsonAsync("/AuthenticationService/Login", authenticationInfo);
            var responce = await _client.GetAsync($"/AuthenticationService/CheckAuthorize");

            // Assert
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }
    }
}
