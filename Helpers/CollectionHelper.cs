using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Vardirsoft.Shared.API;

namespace Vardirsoft.Shared.Helpers
{
    public static class CollectionHelper
    {
        public static bool HasItems(this Array array) => array != null && array.Length > 0;
        public static bool HasItems<T>(this T[] array) => array != null && array.Length > 0;
        public static bool HasItems(this ICollection collection) => collection != null && collection.Count > 0;
        public static bool HasItems<T>(this ICollection<T> collection) => collection != null && collection.Count > 0;

        public static bool Contains<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var item in collection)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        }

        public static bool AnyNotIn<T>(this IEnumerable<T> collection, IEnumerable<T> another)
            where T: IComparable<T>
        {
            if (another == null)
                throw new ArgumentNullException(nameof(another));

            var found = false;
            foreach (var item in collection)
            {
                foreach (var item2 in another)
                {
                    if (item.CompareTo(item2) == 0)
                    {    
                        found = true;
                    }
                }
            }

            return !found;
        }
        public static bool AnyNotIn<T, Y>(this IEnumerable<T> collection, Func<T, Y> extractor, IEnumerable<Y> another)
            where Y : IComparable<Y>
        {
            if (another == null)
                throw new ArgumentNullException(nameof(another));

            if (extractor == null)
                throw new ArgumentNullException(nameof(extractor));

            var found = false;
            foreach (var item in collection)
            {
                foreach (var item2 in another)
                {
                    var extracted = extractor(item);
                    if (extracted.CompareTo(item2) == 0)
                    {    
                        found = true;
                    }
                }
            }
            
            return !found;
        }
        public static bool AnyNotIn<T, Y>(this IEnumerable<T> collection, IEnumerable<Y> another, Func<Y, T> extractor)
            where T : IComparable<T>
        {
            if (another == null)
                throw new ArgumentNullException(nameof(another));

            if (extractor == null)
                throw new ArgumentNullException(nameof(extractor));

            var found = false;
            foreach (var item in collection)
            {
                foreach (var item2 in another)
                {
                    var extracted = extractor(item2);
                    if (extracted.CompareTo(item) == 0)
                    {    
                        found = true;
                    }
                }
            }

            return !found;
        }

        public static int IndexWith<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

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

        public static int LastIndexWith<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var i = 0;
            var last = -1;

            foreach (var item in collection)
            {
                if (predicate(item))
                {    
                    last = i;
                }

                i++;
            }

            return last;
        }
        
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in collection)
            {
                action(item);
            }
        }
        public static void ForEachI<T>(this IEnumerable<T> collection, Action<int, T> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var i = 0;
            foreach (var item in collection)
            {
                action(i, item);
                i++;
            }
        }
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
                
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {    
                collection.Add(item);
            }
        }

        public static void Insert<T>(this IList<T> items, T item)
            where T: IOrderable
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.OrdinalIndex <= 0)
            {
                var firstIndex = items.LastIndexWith(i => i.OrdinalIndex <= item.OrdinalIndex);
                if (firstIndex == -1)
                {
                    items.Insert(0, item);
                }
                else
                {
                    items.Insert(firstIndex + 1, item);
                }
            }
            else
            {
                var firstIndex = items.IndexWith(i => i.OrdinalIndex >= item.OrdinalIndex);
                if (firstIndex == -1)
                {
                    items.Add(item);
                }
                else
                {
                    items.Insert(firstIndex, item);
                }
            }
        }

        public static void Remove<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }
        public static void Remove<T>(this LinkedList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var node = list.First;
            while (node.HasValue())
            {
                var next = node.Next;
                if (predicate(node.Value))
                {    
                    list.Remove(node);
                }

                node = next;
            }
        }

        public static Y Fold<T, Y>(this IEnumerable<T> collection, Func<T, Y, Y> mapFunc, Y initial)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (mapFunc == null)
                throw new ArgumentNullException(nameof(mapFunc));

            if (collection.Any())
            {
                var acc = initial; //TODO: huli skip
                foreach (var item in collection)
                {
                    acc = mapFunc(item, acc);
                }

                return acc;
            }

            return initial;
        }

        public static TResult[] Select<T, TResult>(this ICollection<T> collection, Func<T, TResult> transform)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            var result = new TResult[collection.Count];
            var i = 0;

            foreach (var item in collection)
            {    
                result[i++] = transform(item);
            }

            return result;
        }
    }
}