using eventz.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eventz.Data.Map
{
    public class HomeMap : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey("UserId");
            builder.HasMany(x => x.Sections);
            builder.HasMany(x => x.Categories);
        }
    }
}
