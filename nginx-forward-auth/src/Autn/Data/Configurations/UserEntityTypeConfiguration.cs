﻿using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired();
            builder.Property(m => m.Login).HasColumnName("login").IsRequired();
            builder.Property(m => m.Password).HasColumnName("password").IsRequired();
            builder.Property(m => m.Email).HasColumnName("email").IsRequired();
            builder.Property(m => m.LastName).HasColumnName("last_name").IsRequired();
            builder.Property(m => m.FirstName).HasColumnName("first_name").IsRequired();
        }
    }
}