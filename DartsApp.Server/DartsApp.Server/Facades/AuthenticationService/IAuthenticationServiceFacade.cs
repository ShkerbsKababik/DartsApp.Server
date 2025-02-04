namespace DartsApp.Server.Facades.AuthenticationService
{
    public interface IAuthenticationServiceFacade
    {
        public void Login(AuthenticationInfo authenticationInfo);
        public void Logout();
    }
    public class AuthenticationInfo
    { 
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
