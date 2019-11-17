using System;

namespace Vardirsoft.Shared.Helpers
{
    public static class EnumHelper
    {
        public static bool HasKey<T>(string value, out T key)
            where T: struct
        {
            var type = typeof(T);

            key = default;
            
            if (!type.IsEnum)
                throw new ArgumentException("The specified argument is not an Enum subtype", nameof(type));

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var names = Enum.GetNames(type);
            var compareTo = value.ToLower();

            foreach (var x in names)
            {
                var name = x.ToLower();

                if (name == compareTo)
                {
                    Enum.TryParse(x, out key);
                    return true;
                }
            }

            return false;
        }
    }
}