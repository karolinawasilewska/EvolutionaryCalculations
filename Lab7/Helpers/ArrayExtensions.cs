using System;

namespace Lab8.Helpers
{
    public static class ArrayExtensions
    {
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
        public static int IndexOf(this City[] source, City element)
        {
            int index = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].Index == element.Index)
                {
                    return i; 
                }
            }

            return index;
        }
    }
}
