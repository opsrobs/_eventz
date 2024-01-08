using System.ComponentModel.DataAnnotations.Schema;

namespace eventz.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]

        public IFormFile ImageFile { get; set; }

        public string LocalizationDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? EventDescription { get; set; }
        public Guid localizationId { get; set; }
        public Localization localization { get; set; }

        public Event(Guid id, string? name, string? type, string imageUrl, IFormFile imageFile, string localizationDescription, DateTime startDate, DateTime endDate, string? eventDescription, Guid localizationId, Localization localization)
        {
            Id = id;
            Name = name;
            Type = type;
            ImageUrl = imageUrl;
            ImageFile = imageFile;
            LocalizationDescription = localizationDescription;
            StartDate = startDate;
            EndDate = endDate;
            EventDescription = eventDescription;
            this.localizationId = localizationId;
            this.localization = localization;
        }

        public Event()
        {
        }
    }
}
