using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BPCode.Helpers
{
    public static class CollectionHelper
    {
        public static bool HasItems(this Array array) => array != null && array.Length > 0;
        public static bool HasItems<T>(this T[] array) => array != null && array.Length > 0;
        public static bool HasItems(this ICollection collection) => collection != null && collection.Count > 0;
        public static bool HasItems<T>(this ICollection<T> collection) => collection != null && collection.Count > 0;

        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var i = 0;
            foreach (var item in collection)
            {
                if (predicate(item))
                    return i;
                    
                i++;
            }

            return -1;
        }
        
        public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var i = 0;
            foreach (var item in collection)
            {
                action(i++, item);
            }
        }

        public static Y Fold<T, Y>(this IEnumerable<T> collection, Func<T, Y, Y> mapFunc, Y initial)
        {
            if (mapFunc == null)
                throw new ArgumentNullException(nameof(mapFunc));

            if (collection == null || !collection.Any())
                return initial;

            var acc = initial;

            foreach (var item in collection.Skip(1))
            {
                acc = mapFunc(item, acc);
            }

            return acc;
        }
    }
}