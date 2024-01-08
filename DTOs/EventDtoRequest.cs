using eventz.Models;

namespace eventz.DTOs
{
    public record EventDtoRequest(string? Name, string? Type, IFormFile ImageFile, string LocalizationDescription, DateTime StartDate, DateTime EndDate, string? EventDescription, LocalizationDto localization)
    {
    }

}
