using PTS.Core.Plugins.Authentication.Models;

namespace PTS.Core.Plugins.Authentication
{
    public interface IUserService
    {
        UserInfo UserInfo { get; }
        int PersonnelId { get; }
        bool IsAdmin { get; }
        bool IsLogin { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
       
    }
}
