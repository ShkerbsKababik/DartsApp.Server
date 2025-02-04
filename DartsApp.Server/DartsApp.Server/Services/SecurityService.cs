using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace DartsApp.Server.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private List<Person> people;

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            people = new List<Person>();

            var person1 = new Person
                (
                    login: "admin",
                    password: "admin",
                    roles: new List<Role> { new Role("admin"), new Role("user") }
                );

            var person2 = new Person
                (
                    login: "asd",
                    password: "asd",
                    roles: new List<Role> { new Role("user") }
                );
        }

        public void Login(string login, string password)
            => LoginAsync(login, password).Wait();

        public async Task LoginAsync(string login, string password)
        {
            Person? person = people.FirstOrDefault(p => p.Login== login && p.Password == password);

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
