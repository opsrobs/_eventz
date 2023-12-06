using eventz.Models;

namespace eventz.DTOs
{
    public record HomeDto
    {
        public List<CategoryDtoRequest> Categories { get; init; }
        public List<SectionDtoRequest> Sections { get; init; }
    }

}
