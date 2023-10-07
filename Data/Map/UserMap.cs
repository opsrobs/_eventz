using eventz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventz.Data.Map
{

    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CPF).HasMaxLength(20);
            builder.Property(x => x.DateOfBirth).HasMaxLength(20);
            builder.HasOne(x => x.Person);
        }
    }
}
