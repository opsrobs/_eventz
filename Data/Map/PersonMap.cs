﻿using eventz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventz.Data.Map
{
    public class PersonMap : IEntityTypeConfiguration<PersonModel>
    {
        public void Configure(EntityTypeBuilder<PersonModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Email).HasMaxLength(65);
            builder.Property(x => x.CreatedAt).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Roles);
        }
    }
}
