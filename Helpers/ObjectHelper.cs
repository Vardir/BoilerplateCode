using System;
using System.Collections.Generic;

namespace Vardirsoft.Shared.Helpers
{
    public static class ObjectHelper
    {
        public static Maybe<T> As<T>(this object obj)
        {
            if (obj is T tObj)
                return new Maybe<T>(tObj);
            
            return new Maybe<T>();
        }
        
        public static T[] AsArray<T>(this T obj) => new[] { obj };
        
        public static List<T> AsList<T>(this T obj) => new List<T> { obj };
        
        public static IEnumerable<T> AsIEnumerable<T>(this T obj)
        {
            yield return obj;
        }

        public static bool HasValue(this object obj) => obj != null;

        public static bool IsNot<T>(this object obj) => !(obj is T);

        public static bool Is<T1, T2>(this object obj) => obj is T1 || obj is T2;
        public static bool Is<T1, T2, T3>(this object obj) => obj is T1 || obj is T2 || obj is T3;
        public static bool Is<T1, T2, T3, T4>(this object obj) => obj is T1 || obj is T2 || obj is T3 || obj is T4;
        public static bool Is<T1, T2, T3, T4, T5>(this object obj) => obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5;
        public static bool Is<T1, T2, T3, T4, T5, T6>(this object obj) => obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5 || obj is T6;
        public static bool Is<T1, T2, T3, T4, T5, T6, T7>(this object obj) => obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5 || obj is T6 || obj is T7;
        public static bool Is<T1, T2, T3, T4, T5, T6, T7, T8>(this object obj) => obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5 || obj is T6 || obj is T7 || obj is T8;
    }

    public struct Maybe<T>
    {
        public readonly bool HasValue;
        public readonly T Value;

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }

        public void Deconstruct(out bool hasValue, out T value)
        {
            hasValue = HasValue;
            value = Value;
        }

        public void Switch(Action<T> actionIfHasValue, Action actionIfNoValue)
        {
            if (HasValue)
            {
                actionIfHasValue(Value);
            }
            else
            {
                actionIfNoValue();
            }
        }
        
        public Maybe<TProp> Get<TProp>(Func<T, TProp> extractor)
        {
            return HasValue ? new Maybe<TProp>(extractor(Value)) : new Maybe<TProp>();
        }

        public TProp UnsafeGet<TProp>(Func<T, TProp> extractor) => extractor(Value);
    }
}