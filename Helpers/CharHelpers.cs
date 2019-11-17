namespace Vardirsoft.Shared.Helpers
{
    public static class CharHelpers
    {
        public static char[] To(this char c, char last)
        {
            var count = last - c + 1;
            if (count <= 0)
                return new[] { c };

            var array = new char[count];
            var start = c;
            for (var i = 0; i < count; i++)
            {
                array[i] = (char)(start + i);
            }
            
            return array;
        }
    }
}