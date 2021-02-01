using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel model);
        Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model);
        Task<IResponse> LogoutAsync();
        Task<IResponse> ChangePasswordAsync(ChangePasswordModel model);
    }
}
