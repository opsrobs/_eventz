using eventz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventz.Data.Map
{
    public class EventMap: IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.ImageUrl).IsRequired();
            builder.Property(e => e.LocalizationDescription).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();
            builder.Property(e => e.EventDescription).IsRequired();
            builder.HasOne(e => e.localization);
        }
    }
}
