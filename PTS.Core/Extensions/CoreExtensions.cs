using System;

namespace PTS.Core.Extenstions
{
    public static class CoreExtensions
    {
        public static int ToInt(this object value)
        {
            value ??= "0";
            int.TryParse(value.ToString(), out var result);
            return result;
        }
        public static string Right(this string value, int length)
        {
            var result = string.IsNullOrEmpty(value) ? "" : value;
            result = result.Length > length ? result.Substring(result.Length - length, length) : result;
            return result;
        }
        public static string Left(this string value, int length)
        {
            var result = string.IsNullOrEmpty(value) ? "" : value;
            result = result.Length > length ? result.Substring(0, length) : result;
            return result;
        }
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
