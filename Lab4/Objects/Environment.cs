using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.Objects
{
   public class Environment
    {
        public static double Function(double genotype)
        {
            return genotype * Math.Sin(genotype) * Math.Sin(10 * genotype);
        }
    }
}
