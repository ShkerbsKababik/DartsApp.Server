using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace DartsApp.Server.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private List<Person> people = new List<Person>();

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var person = new Person
                (
                    login: "admin",
                    password: "admin",
                    roles: new List<Role> { new Role("admin"), new Role("user") }
                );

            people.Add(person);
        }

        public void Login(string login, string password)
            => LoginAsync(login, password).Wait();

        public async Task LoginAsync(string login, string password)
        {
            var person = people.FirstOrDefault(p => p.Login == login && p.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, person.Login));
                foreach (var role in person.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            }
        }

        public void Logout()
            => LogoutAsync().Wait();

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
