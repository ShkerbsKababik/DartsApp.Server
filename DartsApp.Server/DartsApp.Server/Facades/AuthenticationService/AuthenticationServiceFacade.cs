using DartsApp.Server.Services;

namespace DartsApp.Server.Facades.AuthenticationService
{
    public class AuthenticationServiceFacade : IAuthenticationServiceFacade
    {
        private readonly ISecurityService _securityService;

        public AuthenticationServiceFacade(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public void Login(AuthenticationInfo authenticationInfo)
        {
            _securityService.Login(authenticationInfo.Login, authenticationInfo.Password);
        }

        public void Logout()
        {
            _securityService.Logout();
        }
    }
}
