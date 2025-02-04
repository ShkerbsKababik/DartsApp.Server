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
            
        }

        public void Logout()
        {
            
        }
    }
}
