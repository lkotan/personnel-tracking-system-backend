using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.Rule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IRuleService: IServiceRepository<RuleModel>
    {
        Task<IEnumerable<RuleListModel>> GetAllAsync(int roleId);

        Task<IEnumerable<IResponse>> SaveRangeAsync(IEnumerable<RuleModel> models);
    }
}
