using eventz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventz.Data.Map
{
    public class LocalizationMap: IEntityTypeConfiguration<Localization>
    {
        public void Configure(EntityTypeBuilder<Localization> builder)
    {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Latitude).IsRequired();
            builder.Property(e => e.Longitude).IsRequired();
    }
}
}
