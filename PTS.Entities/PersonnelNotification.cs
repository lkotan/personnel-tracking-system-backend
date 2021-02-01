using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Entities
{
    public class PersonnelNotification:IBaseEntity
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; } = false;


        public Personnel Personnel { get; set; }
        public Notification Notification { get; set; }
    }
}
