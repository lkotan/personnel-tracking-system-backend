using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.DataAccess.Mappings.EF
{
    public class AssigmentMap : IEntityTypeConfiguration<Assigment>
    {
        public void Configure(EntityTypeBuilder<Assigment> builder)
        {
            builder.ToTable("Assigments");

            builder.HasMany(x => x.Notifications).WithOne(x => x.Assigment).HasForeignKey(x => x.AssigmentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
