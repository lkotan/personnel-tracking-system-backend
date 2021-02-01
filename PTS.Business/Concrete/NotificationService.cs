using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Notification;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly IDataAccessRepository<Notification> _dal;
        private readonly IDataAccessRepository<Personnel> _dalPersonnel;
        private readonly IMapper _mapper;
        private readonly IDataAccessRepository<PersonnelNotification> _dalPersonnelNotify;

        public NotificationService(IDataAccessRepository<Notification> dal,IDataAccessRepository<PersonnelNotification> dalPersonnelNotify, IMapper mapper,IDataAccessRepository<Personnel> dalPersonnel)
        {
            _dal = dal;
            _dalPersonnelNotify = dalPersonnelNotify;
            _mapper = mapper;
            _dalPersonnel = dalPersonnel;
        }


        [IsAdminAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
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
        public async Task<IEnumerable<NotificationListModel>> GetAll()
        {
            return await _dal.TableNoTracking
                .Select(x => new NotificationListModel
                {
                    Id=x.Id,
                    Message=x.Message,
                    Title=x.Title,
                    CreatedAt=x.CreatedAt,
                    AssigmentId=x.AssigmentId,
                    PersonnelId=x.PersonnelId,

                    AssigmentTitle=x.AssigmentId!=null ? x.Assigment.Title : null,
                    
                    PersonnelInfo=x.PersonnelId==null ? new PersonnelInfoModel() : new PersonnelInfoModel
                    {
                        Id=x.Personnel.Id,
                        FirstName=x.Personnel.FirstName,
                        LastName=x.Personnel.LastName,
                        ProfilePhoto=x.Personnel.ProfilePhoto,
                    }

                }).ToListAsync();
        }

        [SecurityAspect]
        public async Task<NotificationModel> GetAsync(int id)
        {
           var data= _mapper.Map<NotificationModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
            return data;
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(NotificationValidator))]
        public async Task<IDataResponse<int>> InsertAsync(NotificationModel model)
        {
            if(model.PersonnelId==null)
            {
                var personnels = await _dalPersonnel.TableNoTracking.Where(x=>x.PersonnelType!=PersonnelType.Admin).ToListAsync();

                var notify = await _dal.InsertAsync(_mapper.Map<Notification>(model));
                foreach (var item in personnels)
                {
                    await _dalPersonnelNotify.InsertAsync(new PersonnelNotification
                    {
                        NotificationId = notify.Data,
                        PersonnelId = (int)item.Id
                    });
                }
                return notify;
            }

            var result = await _dal.InsertAsync(_mapper.Map<Notification>(model));
            if (!result.Success) return new ErrorDataResponse<int>();
            await _dalPersonnelNotify.InsertAsync(new PersonnelNotification
            {
                NotificationId = result.Data,
                PersonnelId = (int)model.PersonnelId,
            });
            return result;
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(NotificationValidator))]
        public async Task<IResponse> UpdateAsync(NotificationModel model)
        {
            if (model.PersonnelId != null)
            {
                var entity = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (model.PersonnelId != entity.PersonnelId)
                {
                    var personNotify = await _dalPersonnelNotify.TableNoTracking.FirstOrDefaultAsync(x => x.NotificationId == model.Id);
                    personNotify.IsRead = false;
                    personNotify.PersonnelId = (int)model.PersonnelId;
                    await _dalPersonnelNotify.UpdateAsync(personNotify);
                }
            }
            return await _dal.UpdateAsync(_mapper.Map<Notification>(model));
        }
    }
}
