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
        [Authorize]
        public void Logout()
        {
            _authenticationServiceFacade.Logout();
        }

        [HttpGet]
        [AllowAnonymous]
        public string CheckAnonymous()
        {
            var requestAccept = HttpContext.Request.Headers.Accept;
            var responceAccept = HttpContext.Response.Headers.Accept;
            return $"allowed anonymous";
        }

        [HttpGet]
        [Authorize]
        public string CheckAuthorize()
        {
            return $"allowed authorize";
        }
    }
}
