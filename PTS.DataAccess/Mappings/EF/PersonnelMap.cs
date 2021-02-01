using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.DataAccess.Mappings
{
    public class PersonnelMap : IEntityTypeConfiguration<Personnel>
    {
        public void Configure(EntityTypeBuilder<Personnel> builder)
        {
            builder.ToTable("Personnels");

            builder.HasMany(x => x.Assigments).WithOne(x => x.Personnel).HasForeignKey(x => x.PersonnelId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.RefreshToken).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.RefreshToken).HasMaxLength(255);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
        }
    }
}
