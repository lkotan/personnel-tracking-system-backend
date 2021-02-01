using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Models.EmailParameter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IEmailParameterService: IServiceRepository<EmailParameterModel>
    {
        Task<IEnumerable<EmailParameterModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
    }
}
