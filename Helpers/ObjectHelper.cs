namespace BPCode.Helpers
{
    public static class ObjectHelper
    {
        public static bool HasValue(this object obj) => obj != null;

        public static bool IsNot<T>(this object obj) => !(obj is T);
    }
}