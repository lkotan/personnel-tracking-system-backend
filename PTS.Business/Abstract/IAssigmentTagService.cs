using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Models.AssigmentTag;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IAssigmentTagService:IServiceRepository<AssigmentTagModel>
    {
        Task<IEnumerable<AssigmentTagModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
     }
}
