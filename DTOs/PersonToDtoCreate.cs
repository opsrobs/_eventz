using eventz.Utils;
using System.ComponentModel.DataAnnotations;

namespace eventz.DTOs
{
    public record PersonToDtoCreate
    {
        public string Name { get; init; }
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        public string Password { get; init; }
        

        public PersonToDtoCreate() { }

        public PersonToDtoCreate(string name, string email,  string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
