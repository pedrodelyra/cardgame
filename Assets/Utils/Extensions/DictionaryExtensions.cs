using System.Collections.Generic;

namespace Utils.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValueOrDefault<TK, T>(this IDictionary<TK, T> source, TK key, T defaultValue = default)
        {
            if (source.TryGetValue(key, out T value))
            {
                return value;
            }
            return source[key] = defaultValue;
        }
    }
}
