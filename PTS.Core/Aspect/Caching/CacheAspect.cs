using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PTS.Core.Extenstions;
using PTS.Core.Plugins.Caching;
using PTS.Core.Utilities.Interceptors;
using PTS.Core.Utilities.IoC;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PTS.Core.Aspect.Caching
{
    public class CacheAspect:MethodInterception
    {
        private readonly int _db;
        private readonly int? _duration;
        private readonly ICacheService _cacheService;
        public CacheAspect(int duration = 60, int db = 0)
        {
            _duration = duration;
            _db = db;
            Priority = 3;
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }
        private static string Key(IInvocation invocation)
        {
            var methodName = $"{invocation.InvocationTarget.GetType().Name.Replace("Service", "")}.{invocation.Method.Name.Replace("Async", "")}";
            var arguments = invocation.Arguments.ToList();
            var parameters = "";
            for (var i = 0; i < arguments.Count; i++)
            {
                parameters += (parameters == "" ? "" : ",") + (arguments[i]?.GetType().IsClass ?? false ? GetPropertyList(arguments[i]) : arguments[i] ?? "Null");
            }
            return $"{methodName}({parameters})";
        }
        private static string GetPropertyList(object entity)
        {
            return entity == null ? "" :
                entity.GetType().GetProperties()
                    .Select(property => property.GetValue(entity) ?? "Null")
                    .Aggregate("", (current, value) => current + (current == "" ? "" : ",") + $"{value}");

        }
        public override void Intercept(IInvocation invocation)
        {
            if (!_cacheService.IsEnabled)
            {
                invocation.Proceed();
                return;
            }
            var method = invocation.MethodInvocationTarget;
            var key = Key(invocation);
            if (_cacheService.Any(key).Result)
            {
                var resultType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
                var returnValue = _cacheService.Get(key).Result;
                dynamic temp = JsonConvert.DeserializeObject(returnValue, resultType);
                invocation.ReturnValue = Task.FromResult(temp);
                return;
            }
            invocation.Proceed();
            var isAsync = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            if (isAsync && typeof(Task).IsAssignableFrom(method.ReturnType))
            {
                invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, key);
            }
        }
        private async Task InterceptAsync(Task task, string key)
        {
            await task.ConfigureAwait(false);
        }
        private async Task<T> InterceptAsync<T>(Task<T> task, string key)
        {
            var result = await task.ConfigureAwait(false);
            await _cacheService.Set(key, result.ToJson(), _duration, _db);
            return result;
        }
    }
}
