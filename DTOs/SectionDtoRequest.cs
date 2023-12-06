using eventz.Models;

namespace eventz.DTOs
{
    public record SectionDtoRequest(string SectionName, List<EventDtoRequest> Events)
    {
    }
}
