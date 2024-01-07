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
        public DateTime TimeDescription { get; set; }
        public string? EventDescription { get; set; }
        public Guid ThisLocalizationId { get; set; }
        public Localization ThisLocalization { get; set; }

        public Event(Guid id, string? name, string? type, string imageUrl, IFormFile imageFile, string localizationDescription, DateTime timeDescription, string? eventDescription, Guid thisLocalizationId, Localization thisLocalization)
        {
            Id = id;
            Name = name;
            Type = type;
            ImageUrl = imageUrl;
            ImageFile = imageFile;
            LocalizationDescription = localizationDescription;
            TimeDescription = timeDescription;
            EventDescription = eventDescription;
            ThisLocalizationId = thisLocalizationId;
            ThisLocalization = thisLocalization;
        }

        public Event(Guid id, string? name, string? type, string imageUrl, string localizationDescription, DateTime timeDescription, string? eventDescription, Localization localization)
        {
            Id = id;
            Name = name;
            Type = type;
            ImageUrl = imageUrl;
            LocalizationDescription = localizationDescription;
            TimeDescription = timeDescription;
            EventDescription = eventDescription;
            ThisLocalization = localization;
        }

        public Event()
        { 
        }

    }
}
