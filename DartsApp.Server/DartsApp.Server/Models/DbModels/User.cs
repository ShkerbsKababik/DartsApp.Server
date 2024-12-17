namespace DartsApp.Server.Models.DbModel
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
