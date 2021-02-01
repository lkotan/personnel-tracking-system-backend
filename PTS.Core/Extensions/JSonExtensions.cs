using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PTS.Core.Extenstions
{
    public static class JSonExtensions
    {
        public static string ToJson(this object item)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T FromJson<T>(this string item)
        {
            return JsonConvert.DeserializeObject<T>(item);
        }
    }
}
