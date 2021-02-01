using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Messages;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Personnel;
using PTS.Models.PersonnelNotificaion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    public class PersonnelNotificationService : IPersonnelNotificationService
    {
        private readonly IDataAccessRepository<PersonnelNotification> _dal;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PersonnelNotificationService(IDataAccessRepository<PersonnelNotification> dal,IUserService userService,IMapper mapper)
        {
            _dal = dal;

            _userService = userService;

            _mapper = mapper;
        }

        [SecurityAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            if (entity.PersonnelId != _userService.PersonnelId && _userService.IsAdmin)
                return new ErrorDataResponse<int>(AspectMessage.AccessDenied);

            return await _dal.DeleteAsync(entity);
        }

        [IsAdminAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }

        [SecurityAspect]
        public async Task<IEnumerable<PersonnelNotificationListModel>> GetAllAsync(int notificationId)
        {
            return await _dal.TableNoTracking
                .Where(x => x.NotificationId == notificationId)
                .Select(x => new PersonnelNotificationListModel
                {
                    Id=x.Id,
                    CreatedAt=x.Notification.CreatedAt,
                    IsRead=x.IsRead,
                    NotificationMessage=x.Notification.Message,
                    NotificationTitle=x.Notification.Title,
                    PersonnelInfo=new PersonnelInfoModel
                    {
                        Id=x.PersonnelId,
                        FirstName=x.Personnel.FirstName,
                        LastName=x.Personnel.LastName,
                        ProfilePhoto=x.Personnel.ProfilePhoto
                    }
                }).ToListAsync();
        }

        [SecurityAspect]
        public async Task<IEnumerable<PersonnelNotificationListModel>> GetAllByPersonnelIdAsync(int personnelId)
        {
            return await _dal.TableNoTracking
               .Where(x => x.PersonnelId == personnelId)
               .Select(x => new PersonnelNotificationListModel
               {
                   Id = x.Id,
                   CreatedAt = x.Notification.CreatedAt,
                   IsRead = x.IsRead,
                   NotificationMessage = x.Notification.Message,
                   NotificationTitle = x.Notification.Title,
                   PersonnelInfo = new PersonnelInfoModel
                   {
                       Id = x.PersonnelId,
                       FirstName = x.Personnel.FirstName,
                       LastName = x.Personnel.LastName,
                       ProfilePhoto = x.Personnel.ProfilePhoto
                   }
               }).ToListAsync();
        }

        [SecurityAspect]
        public async Task<IEnumerable<PersonnelNotificationListModel>> GetAllMyNotificatioAsync()
        {
            return await _dal.TableNoTracking
               .Where(x => x.PersonnelId == _userService.PersonnelId)
               .Select(x => new PersonnelNotificationListModel
               {
                   Id = x.Id,
                   NotificationId=x.NotificationId,
                   CreatedAt = x.Notification.CreatedAt,
                   IsRead = x.IsRead,
                   NotificationMessage = x.Notification.Message,
                   NotificationTitle = x.Notification.Title,
                   PersonnelInfo = new PersonnelInfoModel
                   {
                       Id = x.PersonnelId,
                       FirstName = x.Personnel.FirstName,
                       LastName = x.Personnel.LastName,
                       ProfilePhoto = x.Personnel.ProfilePhoto
                   }
               }).ToListAsync();
        }

        [IsAdminAspect]
        public async Task<PersonnelNotificationModel> GetAsync(int id)
        {
            return _mapper.Map<PersonnelNotificationModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(PersonnelNotificationValidator))]
        public async Task<IDataResponse<int>> InsertAsync(PersonnelNotificationModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<PersonnelNotification>(model));
        }

        [SecurityAspect]
        public async Task<IResponse> IsReadAsync(int id)
        {
            var entity = await _dal.GetAsync(x => x.Id == id);
            entity.IsRead = true;
            return await _dal.UpdateAsync(entity);
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(PersonnelNotificationValidator))]
        public async Task<IResponse> UpdateAsync(PersonnelNotificationModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<PersonnelNotification>(model));
        }
    }
}
