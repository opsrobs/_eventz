namespace eventz.DTOs
{
    public record UserToDtoUpdate(string CPF, DateTime? DateOfBirth, PersonToDtoUpdate Person);

}
