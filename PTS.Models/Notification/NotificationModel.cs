using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Notification
{
    public class NotificationModel:IBaseModel
    {
        public int Id { get; set; }
        public int? PersonnelId { get; set; }
        public int? AssigmentId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
