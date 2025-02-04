namespace DartsApp.Server.Services
{
    public interface ISecurityService
    {
        void Login(string login, string password);
        void Logout();
    }

    class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public Person(string login, string password, List<Role> roles)
        {
            Login = login;
            Password = password;
            Roles = roles;
        }
    }
    class Role
    {
        public string Name { get; set; }
        public Role(string name)
        {
            Name = name;
        }
    }
}
