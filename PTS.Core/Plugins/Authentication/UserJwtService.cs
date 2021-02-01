using Microsoft.AspNetCore.Http;
using PTS.Core.Enums;
using PTS.Core.Extenstions;
using PTS.Core.Plugins.Authentication.Models;
using System.Linq;

namespace PTS.Core.Plugins.Authentication
{
    public class UserJwtService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggedInUsers _loggedInUsers;

        public UserJwtService(IHttpContextAccessor httpContextAccessor, LoggedInUsers loggedInUsers)
        {
            _httpContextAccessor = httpContextAccessor;
            _loggedInUsers = loggedInUsers;
        }

        private UserInfo GetUser()
        {
            var userInfo = _httpContextAccessor?.HttpContext?.User;
            var personnelId = userInfo.GetPersonnelId();

            return _loggedInUsers.UserInfo.FirstOrDefault(x => x.PersonnelId == personnelId);
        }

        public UserInfo UserInfo => GetUser();

        public int PersonnelId => GetUser().PersonnelId;

        public bool IsAdmin => PersonnelType == PersonnelType.Admin;

        public bool IsLogin => GetUser() != null;

        public string FirstName => GetUser().FirstName;

        public string LastName => GetUser().LastName;

        public string Email => GetUser().Email;

        public string ProfilPhoto => GetUser().ProfilePhoto;

        private PersonnelType PersonnelType => GetUser().PersonnelType;
    }
}
