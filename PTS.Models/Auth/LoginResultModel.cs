using PTS.Core.Models;
using System;
using System.Collections.Generic;

namespace PTS.Models.Auth
{
    public class LoginResultModel
    {
        public int PersonnelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }

        public List<PersonnelRulesModel> Rules { get; set; }
    }
}
