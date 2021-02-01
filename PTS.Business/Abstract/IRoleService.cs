using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IRoleService:IServiceRepository<RoleModel>
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<IResponse> IsBlockedChangeAsync(int id);
    }
}
