using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Objects
{
    public class Range<T>
    {
        public T Max { get; set; }
        public T Min { get; set; }
        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
   
}
