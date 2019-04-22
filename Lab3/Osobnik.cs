using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Osobnik
    {
        public uint m_genotyp;
        public double m_fenotyp
        {
            get
            {
                return Fenotyp(this);
            }
            set { }
        }
        public static double Fenotyp(Osobnik osobnik)
        {
            return -2 + osobnik.m_genotyp * 1.0 / 1000000000;
        }
        public static Osobnik NaprawOsobnika(Osobnik osobnik, Dziedzina dziedzina)
        {
            if (FunkcjaDopasowania(osobnik.m_fenotyp) > dziedzina.Max)
                osobnik.m_fenotyp = dziedzina.Max;
            else if (FunkcjaDopasowania(osobnik.m_fenotyp) < dziedzina.Min)
                osobnik.m_fenotyp = dziedzina.Min;

            return osobnik;
        }

        public static double FunkcjaDopasowania(double x)
        {
            return x * Math.Sin(x) * Math.Sin(10 * x);
        }
    }
}
