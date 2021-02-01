using PTS.Core.Repositories;
using PTS.Models.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface INotificationService:IServiceRepository<NotificationModel>
    {
        Task<IEnumerable<NotificationListModel>> GetAll();
    }
}
