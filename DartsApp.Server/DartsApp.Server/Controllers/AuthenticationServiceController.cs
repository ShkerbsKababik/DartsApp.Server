using DartsApp.Server.Facades.AuthenticationService;
using Microsoft.AspNetCore.Authorization;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AuthenticationServiceController : ControllerBase, IAuthenticationServiceFacade
    {
        private readonly IAuthenticationServiceFacade _authenticationServiceFacade;

        public AuthenticationServiceController(IAuthenticationServiceFacade authenticationServiceFacade)
        {
            _authenticationServiceFacade = authenticationServiceFacade;
        }

        [HttpPost]
        public void Login(AuthenticationInfo authenticationInfo)
        {
            _authenticationServiceFacade.Login(authenticationInfo);
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public void Logout()
        {
            _authenticationServiceFacade.Logout();
        }
    }
}
