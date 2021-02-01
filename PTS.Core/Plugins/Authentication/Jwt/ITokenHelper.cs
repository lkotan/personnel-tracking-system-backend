using PTS.Core.Models;
using System.Collections.Generic;

namespace PTS.Core.Plugins.Authentication.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(int personnelId,List<PersonnelRulesModel> rules);
    }
}
