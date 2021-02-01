using PTS.Core.Signatures;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.PersonnelNotificaion
{
    public class PersonnelNotificationListModel:IBaseModel
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public PersonnelInfoModel PersonnelInfo { get; set; }
    }
}
