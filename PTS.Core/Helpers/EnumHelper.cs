using PTS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PTS.Core.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<DropdownModel> List<T>()
        {
            var result = new List<DropdownModel>();
            try
            {
                foreach (var e in Enum.GetValues(typeof(T)))
                {
                    var id = (int)e;
                    var name = e.ToString();
                    var foo = (T)Enum.ToObject(typeof(T), id);
                    var displayValue = GetDisplayValue(foo);
                    result.Add(new DropdownModel
                    {
                        Id = id,
                        Description = displayValue == "" ? name : displayValue
                    });
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string GetDisplayValue<T>(T value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes != null && descriptionAttributes[0].ResourceType != null)
                    return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
                return descriptionAttributes != null && descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
            }
            catch
            {
                return value.ToString();
            }
        }

        private static string LookupResource(IReflect resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    var resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }
            return resourceKey;
        }

    }
}
