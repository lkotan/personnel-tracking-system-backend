using PTS.Core.Signatures;
using PTS.Models.Personnel;
using PTS.Models.PersonnelNotificaion;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Notification
{
    public class NotificationListModel:IBaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int? PersonnelId { get; set; }
        public int? AssigmentId { get; set; }
        public PersonnelInfoModel PersonnelInfo { get; set; }
        public string AssigmentTitle { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
