using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Utilities.Interceptors;
using PTS.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using PTS.Core.Exceptions;
using PTS.Core.Messages;
using System.Linq;
using PTS.Core.Extenstions;
using PTS.Core.Enums;

namespace PTS.Core.Aspect.Security
{
    public class IsAdminAspect:MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private LoggedInUsers _loggedInUsers;
        protected override void OnBefore(IInvocation invocation)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _loggedInUsers = ServiceTool.ServiceProvider.GetService<LoggedInUsers>();

            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var personnelId = user.GetPersonnelId();
            if (personnelId == 0)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var userInfo = _loggedInUsers.UserInfo.FirstOrDefault(x => x.PersonnelId == personnelId);
            if (userInfo == null)
                throw new SecurityException(AspectMessage.AccessDenied);


            var isAdmin = userInfo.PersonnelType == PersonnelType.Admin;
            if (isAdmin)
                return;

            throw new SecurityException(AspectMessage.AccessDenied);

        }
    }
}
