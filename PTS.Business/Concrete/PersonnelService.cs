using AutoMapper;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Caching;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Helpers;
using PTS.Core.Messages;
using PTS.Core.Models;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Plugins.EmailServices;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Chart;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    [SecurityAspect]
    public class PersonnelService : IPersonnelService
    {
        private readonly IDataAccessRepository<Personnel> _dal;
        private readonly IMapper _mapper;


        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public PersonnelService(IDataAccessRepository<Personnel> dal, IMapper mapper, IEmailTemplateService emailTemplateService, IEmailService emailService, IUserService userService)
        {
            _dal = dal;

            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _userService = userService;

            _mapper = mapper;
        }

        [IsAdminAspect]
        [RemoveCacheAspect]
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

        [CacheAspect(20, 0)]
        public async Task<List<PersonnelListModel>> GetAllAsync()
        {
            return await _dal.TableNoTracking
                .Where(x => x.PersonnelType != PersonnelType.Admin)
                .Include(x => x.Title)
                 .Include(x => x.Department)

                .Select(x => new PersonnelListModel
                {
                    Id = x.Id,
                    PersonnelTypeName = EnumHelper.GetDisplayValue(x.PersonnelType),
                    PersonnelType = x.PersonnelType,
                    Email = x.Email,
                    Title = x.Title.Name,
                    Department = x.Department.Name,
                    Gsm = x.Gsm,
                    IsBlocked = x.IsBlocked,
                    PersonnelInfo = new PersonnelInfoModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ProfilePhoto = x.ProfilePhoto
                    }

                }).ToListAsync();
        }
       
        public async Task<PersonnelModel> GetAsync(int id)
        {
            return _mapper.Map<PersonnelModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(PersonnelValidator))]
        [RemoveCacheAspect]
        public async Task<IDataResponse<int>> InsertAsync(PersonnelModel model)
        {
            var entity = _mapper.Map<Personnel>(model);
            var password = Helper.CreatePassword();

            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            entity.RefreshToken = Helper.CreateToken();
            entity.RefreshTokenExpiredDate = DateTime.Now.AddDays(-1);
            var result = await _dal.InsertAsync(entity);

            if (!result.Success) return result;

            var emailTemplate = await _emailTemplateService.GetTemplateAndEmailParameterAsync(EmailTemplateType.CreatePersonnelPasswordNotification);

            emailTemplate.Body = emailTemplate.Body
                .Replace("[Password]", password)
                .Replace("[FullName]", $"{model.FirstName} {model.LastName}");

            var emailResult = await _emailService.SendMailAsync(model.Email, emailTemplate);

            return !emailResult.Success ? new ErrorDataResponse<int>(result.Data, emailResult.Message) : result;
        }

        [ValidationAspect(typeof(PersonnelValidator))]
        [RemoveCacheAspect]
        public async Task<IResponse> UpdateAsync(PersonnelModel model)
        {
            var entity = await _dal.GetAsync(x => x.Id == model.Id);
            if (_userService.IsAdmin)
            {
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Gsm = model.Gsm;
                entity.Email = model.Email;
                entity.PersonnelType = model.PersonnelType;
                entity.RoleId = model.RoleId;
                entity.TitleId = model.TitleId;
                entity.DepartmentId = model.DepartmentId;
            }
            else
            {
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Gsm = model.Gsm;
                entity.Email = model.Email;
            }
            return await _dal.UpdateAsync(entity);
        }

        [IsAdminAspect]
        public async Task<IResponse> IsBlockedChangeAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            entity.IsBlocked = !entity.IsBlocked;
            return await _dal.UpdateAsync(entity);
        }

        public async Task<IResponse> ForgotPasswordAsync(string email)
        {
            var result = await PersonnelIsExsits(email);
            if (!result.Success) return new ErrorResponse(PersonnelMessage.PersonnelNotFound);

            var entity = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == email);
            var password = Helper.CreatePassword();

            var emailTemplate = await _emailTemplateService.PublicGetTemplateAndEmailParameterAsync(EmailTemplateType.ForgotPassword);

            emailTemplate.Body = emailTemplate.Body
                .Replace("[Password]", password)
                .Replace("[FullName]", $"{entity.FirstName} {entity.LastName}");

            var emailResult = await _emailService.SendMailAsync(entity.Email, emailTemplate);


            if (!emailResult.Success) return new ErrorDataResponse<int>();

            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            entity.RefreshToken = Helper.CreateToken();
            entity.RefreshTokenExpiredDate = DateTime.Now.AddDays(-1);
            await _dal.UpdateAsync(entity);

            return new SuccessDataResponse<int>();
        }

        public async Task<IEnumerable<PersonnelSelectListModel>> SelectListAsync()
        {
            var entities = await _dal.TableNoTracking.OrderBy(x => x.FirstName).Where(x=>x.PersonnelType!=PersonnelType.Admin).ToListAsync();

            return entities.Select(x => new PersonnelSelectListModel
            {
                Id = x.Id,
                Description = $"{x.FirstName} {x.LastName}",
                ProfilePhoto = x.ProfilePhoto
            });
        }

        private async Task<IResponse> PersonnelIsExsits(string email)
        {
            var entity = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == email);
            if (entity == null) return new ErrorResponse();
            return new SuccessResponse();
        }

        public async Task<IEnumerable<DropdownModel>> SearchAsync(string keyword)
        {
            keyword = keyword.IsNullOrEmpty() ? "" : keyword.ToLower();

            var entities = await _dal.TableNoTracking
                .Where(x => !x.IsBlocked && x.PersonnelType != PersonnelType.Admin)
                .ToListAsync();
            return entities.Select(x => new DropdownModel
            {
                Id = x.Id,
                Description = $"{x.FirstName} {x.LastName}"
            })
            .Where(x => x.Description.ToLower().Contains(keyword));

        }

        public async Task<ProfileModel> ProfileAsync()
        {
            return await _dal.TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Department)
                .Include(x => x.Assigments)
                .Select(x => new ProfileModel
                {
                    Id = x.Id,
                    PersonnelTypeName = EnumHelper.GetDisplayValue(x.PersonnelType),
                    PersonnelType = x.PersonnelType,
                    Email = x.Email,
                    Title = x.Title.Name,
                    Department = x.Department.Name,
                    Gsm = x.Gsm,
                    IsBlocked = x.IsBlocked,
                    PersonnelInfo = new PersonnelInfoModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ProfilePhoto = x.ProfilePhoto
                    },
                    AssigmentCount = x.Assigments.Count
                }).FirstOrDefaultAsync(x=>x.Id==_userService.PersonnelId);
        }

        public async Task<IEnumerable<DashChartModel>> GetDashChartAsync()
        {
            return await _dal.TableNoTracking
                .Where(x=>x.Id!=1)
                .Select(x => new DashChartModel
                {
                    PersonnelFullName = $"{x.FirstName} {x.LastName}",
                    AssigmentCount = x.Assigments.Count,
                }).ToListAsync();
        }
    }
}
