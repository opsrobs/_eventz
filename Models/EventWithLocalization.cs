namespace eventz.Models
{
    public class EventWithLocalization
    {
        public Event Event { get; set; }
        public Localization Localization { get; set; }

        public EventWithLocalization(Event @event, Localization localization)
        {
            Event = @event;
            Localization = localization;
        }

        public EventWithLocalization()
        {
        }
    }


}
