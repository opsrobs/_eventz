using eventz.Models;

namespace eventz.DTOs
{
    public record EventDtoRequest(string? Name, string? Type, string ImageUrl, string LocalizationDescription, DateTime TimeDescription, string? EventDescription, LocalizationDto ThisLocalization)
    {
    }
}
