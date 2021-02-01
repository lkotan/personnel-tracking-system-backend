using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.PersonnelNotificaion
{
    public class PersonnelNotificationModel:IBaseModel
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public int NotificationId { get; set; }
    }
}
