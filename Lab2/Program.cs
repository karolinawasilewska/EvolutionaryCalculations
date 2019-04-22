// 2019.03.04 "prosta", "niedoskonała" implementacja z wykładu

using System;
using System.Collections.Generic;

namespace PROSTY
{
    class Program
    {
        struct tOsobnik
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
        }

        static Random los = new Random();
        static int liczbaUruchomien = 20;
        static void Main(string[] args)
        {
            List<tOsobnik> listaWyników = new List<tOsobnik>();
            List<tOsobnik> najlepsiPopulacji = new List<tOsobnik>();

            for (int index = 0; index < liczbaUruchomien; index++)
            {
                tOsobnik[] populacja = new tOsobnik[20]; // 20 = zalozony staly rozmiar populacji
                for (int i = 0; i < populacja.Length; ++i)
                {
                    do
                    {
                        populacja[i].m_genotyp = (uint)los.Next(int.MinValue, int.MaxValue);
                    } while (CzyWZakresie(populacja[i]));

                }

                tOsobnik maxUruchomienia = populacja[0];

                int nr_pokolenia = 0;

                tOsobnik maxPopulacji;

                while (nr_pokolenia++ < 1000) // 1000 - zalozona liczba pokolen
                {
                    tOsobnik[] nowa_populacja = new tOsobnik[populacja.Length];
                    maxPopulacji = nowa_populacja[0];
                    for (int i = 0; i < nowa_populacja.Length; i++) // nowe pokolenie
                    {
                        tOsobnik dziecko, mama, tata;



                        do
                        {
                            mama = Selekcja(populacja);
                            tata = Selekcja(populacja);
                            dziecko = Rekombinacja(mama, tata);
                            nowa_populacja[i] = dziecko;
                        } while (CzyWZakresie(nowa_populacja[i]));


                    }
                    najlepsiPopulacji.Add(MaxPopulacji(populacja));
                    populacja = nowa_populacja;
                }

                for (int i = 0; i < populacja.Length; i++)
                {
                    Console.WriteLine("{0} dla {1}",
                        FunkcjaDopasowania(Fenotyp(populacja[i])),
                        Fenotyp(populacja[i]));
                }

                listaWyników.Add(MaxPopulacji(najlepsiPopulacji.ToArray()));


            }

            Console.WriteLine("Średnia z wszystkich uruchomień: {0}", Średnia(listaWyników.ToArray()));
            Console.ReadKey();
        }

        static tOsobnik Selekcja(tOsobnik[] populacji)
        {
            tOsobnik k1 = populacji[los.Next(populacji.Length)];
            tOsobnik k2 = populacji[los.Next(populacji.Length)];

            if (FunkcjaDopasowania(Fenotyp(k1))
                > FunkcjaDopasowania(Fenotyp(k2)))
                return k1;
            else
                return k2;
        }

        static double Fenotyp(tOsobnik osobnik)
        {
            return -2 + osobnik.m_genotyp * 1.0 / 1000000000;
        }

        static double FunkcjaDopasowania(double x)
        {
            return x * Math.Sin(x) * Math.Sin(10 * x);
        }

        static tOsobnik Rekombinacja(tOsobnik mama, tOsobnik tata)
        {
            uint maska = ~0u << los.Next(1, 32);
            tOsobnik dziecko;
            dziecko.m_genotyp = maska & mama.m_genotyp
                | ~maska & tata.m_genotyp;

            if (los.NextDouble() < 0.1) // 0.1 - 10% pr. mutacji - zalozenie
            {
                maska = 1u << los.Next(32);
                dziecko.m_genotyp ^= maska;
            }
            return dziecko;
        }

        static tOsobnik MaxPopulacji(tOsobnik[] populacja)
        {
            tOsobnik maxPopulacji = populacja[0];
            foreach (var item in populacja)
            {
                if (FunkcjaDopasowania(item.m_fenotyp) > FunkcjaDopasowania(maxPopulacji.m_fenotyp))
                {
                    maxPopulacji = item;
                }
            }
            return maxPopulacji;
        }

        static double Średnia(tOsobnik[] wartosci)
        {
            double suma = 0.0;

            foreach (var item in wartosci)
            {
                suma += FunkcjaDopasowania(item.m_fenotyp);
            }

            return suma / wartosci.Length;
        }

        static bool CzyWZakresie(tOsobnik osobnik)
        {
            return osobnik.m_fenotyp > 2.0 || osobnik.m_fenotyp < -2.0;
        }
    }
}
