using DartsApp.Server.Facades.AuthenticationService;
using Microsoft.AspNetCore.Authorization;

namespace DartsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationServiceController : ControllerBase, IAuthenticationServiceFacade
    {
        private readonly IAuthenticationServiceFacade _authenticationServiceFacade;

        public AuthenticationServiceController(IAuthenticationServiceFacade authenticationServiceFacade)
        {
            _authenticationServiceFacade = authenticationServiceFacade;
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public string HelloWorld()
        {
            return $"Hello World";
        }
    }
}
