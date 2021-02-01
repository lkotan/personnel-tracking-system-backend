using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Models;
using PTS.Core.Plugins.EmailServices;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
   
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IDataAccessRepository<EmailTemplate> _dal;
        private readonly IMapper _mapper;

        public EmailTemplateService(IDataAccessRepository<EmailTemplate> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        [IsAdminAspect]
        public async Task<IEnumerable<EmailTemplateListModel>> GetAllAsync()
        {
            return await _dal.TableNoTracking.Include(x => x.EmailParameter).Select(x => new EmailTemplateListModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToListAsync();
        }

        [IsAdminAspect]
        public async Task<EmailTemplateModel> GetAsync(int id)
        {
            return _mapper.Map<EmailTemplateModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(EmailTemplateValidator))]
        public async Task<IDataResponse<int>> InsertAsync(EmailTemplateModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<EmailTemplate>(model));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(EmailTemplateValidator))]
        public async Task<IResponse> UpdateAsync(EmailTemplateModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<EmailTemplate>(model));
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

        [IsAdminAspect]
        public async Task<IEnumerable<DropdownModel>> SelectListAsync()
        {
            var entities = await _dal.TableNoTracking.ToListAsync();
            return entities.Select(x => new DropdownModel
            {
                Id = x.Id,
                Description = x.Title
            });
        }

        [IsAdminAspect]
        public async Task<EmailSetting> GetTemplateAndEmailParameterAsync(EmailTemplateType templateType)
        {
            var entity = await _dal.TableNoTracking.Include(x => x.EmailParameter).FirstOrDefaultAsync(x => x.TemplateType == templateType);
            return new EmailSetting
            {
                Subject = entity?.Title ?? "",
                Body = entity?.MessageTemplate ?? "",
                EmailOptions = new EmailOptions
                {
                    UserName = entity?.EmailParameter?.UserName ?? "",
                    Password = entity?.EmailParameter?.Password ?? "",
                    SmtpServer = entity?.EmailParameter?.SmtpServer ?? "",
                    Port = entity?.EmailParameter?.Port ?? 0,
                    EnableSsl = entity?.EmailParameter?.EnableSsl ?? false
                }
            };
        }


        public async Task<EmailSetting> PublicGetTemplateAndEmailParameterAsync(EmailTemplateType templateType)
        {
            var entity = await _dal.TableNoTracking.Include(x => x.EmailParameter).FirstOrDefaultAsync(x => x.TemplateType == templateType);
            return new EmailSetting
            {
                Subject = entity?.Title ?? "",
                Body = entity?.MessageTemplate ?? "",
                EmailOptions = new EmailOptions
                {
                    UserName = entity?.EmailParameter?.UserName ?? "",
                    Password = entity?.EmailParameter?.Password ?? "",
                    SmtpServer = entity?.EmailParameter?.SmtpServer ?? "",
                    Port = entity?.EmailParameter?.Port ?? 0,
                    EnableSsl = entity?.EmailParameter?.EnableSsl ?? false
                }
            };
        }
    }
}
