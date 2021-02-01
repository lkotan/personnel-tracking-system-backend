using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.DataAccess.Mappings.EF
{
    public class PersonnelNotificationMap : IEntityTypeConfiguration<PersonnelNotification>
    {
        public void Configure(EntityTypeBuilder<PersonnelNotification> builder)
        {
            builder.ToTable("PersonnelNotifications");

            builder.HasOne(x => x.Notification).WithMany(x => x.PersonnelNotifications).HasForeignKey(x => x.NotificationId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.PersonnelId, x.NotificationId }).IsUnique();
        }
    }
}
