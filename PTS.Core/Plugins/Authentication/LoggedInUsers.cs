using PTS.Core.Plugins.Authentication.Models;
using System.Collections.Generic;

namespace PTS.Core.Plugins.Authentication
{
    public class LoggedInUsers
    {
        public LoggedInUsers()
        {
            UserInfo = new List<UserInfo>();
        }
        public List<UserInfo> UserInfo { get; set; }
    }
}
