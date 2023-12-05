namespace eventz.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string LocalizationDescription { get; set; }
        public string TimeDescription { get; set; }
        public string EventDescription { get; set; }
        //public Localization Localization { get; set; }
    }
}
