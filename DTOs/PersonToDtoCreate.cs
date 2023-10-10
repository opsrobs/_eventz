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
        public DateTime? DateOfBirth { get; init; }
        public string? CPF { get; init; }

        public PersonToDtoCreate() { }

        public PersonToDtoCreate(string name, string email,  string password, DateTime? dateOfBirth, string? cPF)
        {
            Name = name;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            CPF = cPF;
        }
    }
}
