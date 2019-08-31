using System;
using System.Collections;
using System.Collections.Generic;

namespace BPCode.Helpers
{
    public static class CollectionHelper
    {
        public static bool HasItems(this Array array) => array != null && array.Length > 0;
        public static bool HasItems<T>(this T[] array) => array != null && array.Length > 0;
        public static bool HasItems(this ICollection collection) => collection != null && collection.Count > 0;
        public static bool HasItems<T>(this ICollection<T> collection) => collection != null && collection.Count > 0;
    }
}