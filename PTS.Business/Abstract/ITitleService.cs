using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Models.Title;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface ITitleService:IServiceRepository<TitleModel>
    {
        Task<IEnumerable<TitleModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
    }
}
