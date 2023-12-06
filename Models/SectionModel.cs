namespace eventz.Models
{
    public class Section
    {
        public Guid Id { get; set; }
        public string SectionName { get; set; }
        public List<Event> Events { get; set; }

        public Section(Guid id, string sectionName, List<Event> events)
        {
            Id = id;
            SectionName = sectionName;
            Events = events;
        }

        public Section()
        {
        }
    }
}
