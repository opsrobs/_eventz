using eventz.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eventz.Data.Map
{
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email);
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.RefreshToken).IsRequired();
        }
    }
}
