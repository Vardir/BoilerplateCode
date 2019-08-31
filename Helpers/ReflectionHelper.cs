using System;

namespace BPCode.Helpers
{
    public static class ReflectionHelper
    {
        public static bool HasAttribute<T>(this Type type)
            where T: Attribute
        {
            var attributes = type.GetCustomAttributes(true);

            for (int i = 0; i < attributes.Length; i++)
            {
                var attribute = attributes[i] as Attribute;

                if (attribute is T)
                    return true;
            }

            return false;
        }
    }
}