using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.PersonnelNotificaion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IPersonnelNotificationService:IServiceRepository<PersonnelNotificationModel>
    {
        Task<IEnumerable<PersonnelNotificationListModel>> GetAllAsync(int notificationId);
        Task<IEnumerable<PersonnelNotificationListModel>> GetAllByPersonnelIdAsync(int personnelId);
        Task<IEnumerable<PersonnelNotificationListModel>> GetAllMyNotificatioAsync();
        Task<IResponse> IsReadAsync(int id);
    }
}
