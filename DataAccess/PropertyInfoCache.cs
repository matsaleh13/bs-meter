using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAccess
{
    /// <summary>
    /// Responsible for caching the PropertyInfo objects describing properties of type T.
    /// </summary>
    /// <typeparam name="T">The type from which the PropertyInfo objects are retrieved.</typeparam>
    internal static class PropertyInfoCache<T>
    {
        public static readonly Dictionary<string, PropertyInfo> Value;

        static PropertyInfoCache()
        {
            Value = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(_ => _.Name);
        }
    }
}
