using PTS.Core.Enums;
using PTS.Core.Models;
using PTS.Core.Plugins.EmailServices;
using PTS.Core.Repositories;
using PTS.Models.EmailTemplate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IEmailTemplateService:IServiceRepository<EmailTemplateModel>
    {
        Task<IEnumerable<EmailTemplateListModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
        Task<EmailSetting> GetTemplateAndEmailParameterAsync(EmailTemplateType templateType);

        Task<EmailSetting>PublicGetTemplateAndEmailParameterAsync(EmailTemplateType templateType);
    }
}
