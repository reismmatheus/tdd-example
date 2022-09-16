﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Domain;

namespace TddExample.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TbUser");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.PasswordSalt)
                .IsRequired()
                .HasColumnName("PasswordSalt")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT");

            builder.Property(x => x.CreatedIn)
              .IsRequired()
              .HasColumnName("CreatedIn")
              .HasColumnType("DATETIME");

            builder.Property(x => x.UpdatedIn)
              .HasColumnName("UpdatedIn")
              .HasColumnType("DATETIME");

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("IsDeleted")
                .HasColumnType("BIT");
        }
    }
}
