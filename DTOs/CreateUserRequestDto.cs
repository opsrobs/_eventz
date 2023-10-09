using eventz.Models;

namespace eventz.DTOs
{
    public record CreateUserRequestDto
    {
        public PersonToDtoCreate Person { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? CPF { get; init; }

        public CreateUserRequestDto() { }

        public CreateUserRequestDto(PersonToDtoCreate person, DateTime? dateOfBirth, string? cpf)
        {
            Person = person;
            DateOfBirth = dateOfBirth;
            CPF = cpf;
        }
    }
}