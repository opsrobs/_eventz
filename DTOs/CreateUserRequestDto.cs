using eventz.Models;

namespace eventz.DTOs
{
    public record CreateUserRequestDto
    {
        public PersonModel PersonID { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? CPF { get; init; }

        public CreateUserRequestDto() { }

        
    }
}
