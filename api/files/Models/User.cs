namespace Api.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User WithoutPassword() => new User { Id = Id, Name = Name, Email = Email };

        public override string EntityName => "User";
    }
}