using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Models.Department;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IDepartmentService:IServiceRepository<DepartmentModel>
    {
        Task<IEnumerable<DepartmentModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
    }
}
