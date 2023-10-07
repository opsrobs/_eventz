using eventz.Models;

namespace eventz.DTOs
{
    public record UserToDtoList(Guid Id, DateTime? DateOfBirth, string? CPF, Guid PersonId, PersonDto Person)
    {
    }
}
