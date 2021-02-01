using PTS.Core.Signatures;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PTS.Entities
{
    public class Notification:IBaseEntity
    {
        public int Id { get; set; }
        public int? PersonnelId { get; set; }
        public int? AssigmentId { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Assigment Assigment { get; set; }
        public Personnel Personnel { get; set; }

        public ICollection<PersonnelNotification> PersonnelNotifications { get; set; }
    }
}
