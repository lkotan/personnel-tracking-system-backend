using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.Chart;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IPersonnelService:IServiceRepository<PersonnelModel>
    {
        Task<List<PersonnelListModel>> GetAllAsync();
        Task<IEnumerable<PersonnelSelectListModel>> SelectListAsync();
        Task<IResponse> ForgotPasswordAsync(string email);
        Task<IResponse> IsBlockedChangeAsync(int id);
        Task<IEnumerable<DropdownModel>> SearchAsync(string keyword);
        Task<ProfileModel> ProfileAsync();
        Task<IEnumerable<DashChartModel>> GetDashChartAsync();
    }
}
