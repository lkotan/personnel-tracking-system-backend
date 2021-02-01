using AutoMapper;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Caching;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Exceptions;
using PTS.Core.Helpers;
using PTS.Core.Messages;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Assigment;
using PTS.Models.AssigmentTag;
using PTS.Models.Chart;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    [SecurityAspect]
    public class AssigmentService : IAssigmentService
    {
        private readonly IDataAccessRepository<Assigment> _dal;
        private readonly IDataAccessRepository<Notification> _dalNotify;
        private readonly IDataAccessRepository<PersonnelNotification> _dalPersonnelNotify;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public AssigmentService(IDataAccessRepository<Assigment> dal,IUserService userService,IMapper mapper, IDataAccessRepository<Notification> dalNotify, IDataAccessRepository<PersonnelNotification> dalPersonnelNotify)
        {
            _dal = dal;
            _dalNotify = dalNotify;
            _dalPersonnelNotify = dalPersonnelNotify;

            _userService = userService;

            _mapper = mapper;
        }

        
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            if (entity.PersonnelId != _userService.PersonnelId && !_userService.IsAdmin) return new ErrorDataResponse<int>(AspectMessage.AccessDenied);

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

       
        public async Task<List<AssigmentListModel>> GetAllAsync(int? personnelId,AssigmentStatus? statusId,string keyword)
        {
            return await _dal.TableNoTracking
                .Where(x=>personnelId!=null ? x.PersonnelId==personnelId : x.Id>0)
                .Where(x=>statusId != null ? x.Status == statusId : x.Id > 0)
                .Where(x=>!keyword.IsNullOrEmpty() ? x.Title.ToLower().Contains(keyword.ToLower()) : x.Id>0)
                .Select(x => new AssigmentListModel
                {
                    Id=x.Id,
                    Status=x.Status,
                    StatusName=EnumHelper.GetDisplayValue(x.Status),
                    CreatedAt=x.CreatedAt,
                    DueDate=x.DueDate,
                    HtmlContent=x.HtmlContent,
                    Priority=x.Priority,
                    Title=x.Title,
                    CreatedUser=x.CreatedUser,
                    TagInfo=x.AssigmentTagId==null ? new AssigmentTagModel() : new AssigmentTagModel
                    {
                        Id=x.AssigmentTag.Id,
                        Name=x.AssigmentTag.Name,
                        TagColor=x.AssigmentTag.TagColor,
                        TagBackgroundColor=x.AssigmentTag.TagBackgroundColor
                    },
                    PersonnelInfo=new PersonnelInfoModel
                    {
                        Id=x.Personnel.Id,
                        FirstName=x.Personnel.FirstName,
                        LastName=x.Personnel.LastName,
                        ProfilePhoto=x.Personnel.ProfilePhoto
                    }
                }).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<AssigmentModel> GetAsync(int id)
        {
            return _mapper.Map<AssigmentModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [ValidationAspect(typeof(AssigmentValidator))]
        [RemoveCacheAspect]
        public async Task<IDataResponse<int>> InsertAsync(AssigmentModel model)
        {
            model.CreatedUser = $"{_userService.FirstName} {_userService.LastName}";
            if (model.PersonnelId == 0)
            {
                model.PersonnelId = _userService.PersonnelId;
                return await _dal.InsertAsync(_mapper.Map<Assigment>(model));
            }
            var assigment= await _dal.InsertAsync(_mapper.Map<Assigment>(model));

            if(!assigment.Success) return new ErrorDataResponse<int>(assigment.Message);

            var result=await _dalNotify.InsertAsync(new Notification
            {
                Message = NotificationMessage.Message,
                PersonnelId=model.PersonnelId,
                Title=NotificationMessage.Title,
                AssigmentId=assigment.Data
            });

            if (!result.Success) return new ErrorDataResponse<int>(result.Message);

            await _dalPersonnelNotify.InsertAsync(new PersonnelNotification
            {
                NotificationId=result.Data,
                PersonnelId=model.PersonnelId,
            });

            return assigment;
        }

        
        [ValidationAspect(typeof(AssigmentValidator))]
        [RemoveCacheAspect]
        public async Task<IResponse> UpdateAsync(AssigmentModel model)
        {
            model.CreatedUser = $"{_userService.FirstName} {_userService.LastName}";
            if (model.PersonnelId != _userService.PersonnelId && !_userService.IsAdmin)
                return new ErrorResponse(AspectMessage.AccessDenied);
            return await _dal.UpdateAsync(_mapper.Map<Assigment>(model));
        }


        public async Task<IResponse> ChangePriorityAsync(int assigmentId, short priority)
        {
            var entity = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == assigmentId && x.PersonnelId==_userService.PersonnelId);

            if (entity == null)
                return new ErrorResponse(DbMessage.DataNotFound);
            
            entity.Priority = priority;
            return await _dal.UpdateAsync(entity);
        }

        public async Task<List<AssigmentListModel>> GetAllMyAssigmentAsync()
        {
            return await _dal.TableNoTracking
               .Where(x =>x.PersonnelId == _userService.PersonnelId)
               .Select(x => new AssigmentListModel
               {
                   Id = x.Id,
                   Status = x.Status,
                   StatusName = EnumHelper.GetDisplayValue(x.Status),
                   CreatedAt = x.CreatedAt,
                   DueDate = x.DueDate,
                   HtmlContent = x.HtmlContent,
                   Priority = x.Priority,
                   Title = x.Title,
                   CreatedUser = x.CreatedUser,
                   TagInfo = x.AssigmentTagId == null ? new AssigmentTagModel() : new AssigmentTagModel
                   {
                       Id = x.AssigmentTag.Id,
                       Name = x.AssigmentTag.Name,
                       TagColor = x.AssigmentTag.TagColor,
                       TagBackgroundColor = x.AssigmentTag.TagBackgroundColor
                   },
                   PersonnelInfo = new PersonnelInfoModel
                   {
                       Id = x.Personnel.Id,
                       FirstName = x.Personnel.FirstName,
                       LastName = x.Personnel.LastName,
                       ProfilePhoto = x.Personnel.ProfilePhoto
                   }
               }).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IResponse> ChangeMyStatusAsync(int assigmentId, AssigmentStatus status)
        {
            var entity = await _dal.GetAsync(x => x.Id == assigmentId && x.PersonnelId == _userService.PersonnelId);
            if (entity == null)
                return new ErrorResponse(DbMessage.DataNotFound);
            entity.PersonnelId = _userService.PersonnelId;
            entity.Status = status;

            return await _dal.UpdateAsync(entity);
        }

    }
}
