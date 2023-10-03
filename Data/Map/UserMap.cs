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
            builder.Property(x => x.FirstName).HasMaxLength(128);
            builder.Property(x => x.LastName).HasMaxLength(80);
            builder.Property(x => x.Email).HasMaxLength(80);
            builder.Property(x => x.Username).HasMaxLength(80);
            builder.Property(x => x.CNPJ).HasMaxLength(20);
            builder.Property(x => x.CPF).HasMaxLength(20);
            builder.Property(x => x.DateOfBirth).HasMaxLength(20);
            builder.Property(x => x.CreatedAt).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate();
            builder.Property(x => x.Roles);
        }
    }
}
