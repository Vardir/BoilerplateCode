using System;
using System.Linq;

namespace Vardirsoft.Shared.Helpers
{
    public static class ReflectionHelper
    {
        public static bool HasAttribute<T>(this Type type)
            where T: Attribute
        {
            var attributes = type.GetCustomAttributes(true);

            return Enumerable.Select(attributes, t => t as Attribute).OfType<T>().Any();
        }
    }
}