using eventz.Enums;

namespace eventz.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Password { get; set; }
        public RolesEnum Roles { get; set; }

        public PersonModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public PersonModel(Guid id, string name, string email, DateTime updatedAt,  string password, RolesEnum roles)
        {
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = DateTime.Now;
            UpdatedAt = updatedAt;
            Password = password;
            Roles = roles;
        }
    }
}
