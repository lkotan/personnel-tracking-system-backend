using PTS.Core.Enums;
using PTS.Core.Signatures;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PTS.Entities
{
    public class Assigment: IBaseEntity
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public int? AssigmentTagId { get; set; }

        public AssigmentStatus Status { get; set; } = AssigmentStatus.New;
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public short Priority { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }

        public Personnel Personnel { get; set; }
        public AssigmentTag AssigmentTag { get; set; }

        public ICollection<Notification> Notifications{ get; set; }
    }
}
