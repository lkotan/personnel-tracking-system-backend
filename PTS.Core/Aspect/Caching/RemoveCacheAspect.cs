using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PTS.Core.Plugins.Caching;
using PTS.Core.Utilities.Interceptors;
using PTS.Core.Utilities.IoC;

namespace PTS.Core.Aspect.Caching
{
    public class RemoveCacheAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly int _db;
        private readonly ICacheService _cacheService;

        public RemoveCacheAspect(string pattern = "", int db = 0)
        {
            _db = db;
            _pattern = pattern;
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            var key = _pattern == "" ? $"{invocation.InvocationTarget.GetType().Name.Replace("Service", "")}" : _pattern;
            if (invocation.Method.ReflectedType == null) return;
            _cacheService.RemoveByPattern(key, _db);
        }


    }
}
