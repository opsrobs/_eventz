using eventz.Enums;

namespace eventz.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RolesEnum Roles { get; set; }

        public PersonModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public PersonModel(Guid id, string firstName, string lastName, string email, DateTime createdAt, DateTime updatedAt, string username, string password, RolesEnum roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.Now;
            UpdatedAt = updatedAt;
            Username = username;
            Password = password;
            Roles = roles;
        }
    }
}
