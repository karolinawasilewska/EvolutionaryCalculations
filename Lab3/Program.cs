// 2019.03.04 "prosta", "niedoskonała" implementacja z wykładu

using Lab3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROSTY
{
    class Program
    {


        static Random los = new Random();
        static int liczbaUruchomien = 20;
        static void Main(string[] args)
        {
            List<Osobnik> listaWyników = new List<Osobnik>();
            List<Osobnik> najlepsiPopulacji = new List<Osobnik>();

            for (int index = 0; index < liczbaUruchomien; index++)
            {
                Osobnik[] populacja = new Osobnik[20]; // 20 = zalozony staly rozmiar populacji
                for (int i = 0; i < populacja.Length; ++i)
                {
                    do
                    {
                        populacja[i] = new Osobnik();
                        populacja[i].m_genotyp = (uint)los.Next(int.MinValue, int.MaxValue);
                    } while (CzyWZakresie(populacja[i]));

                }

                Osobnik maxUruchomienia = populacja[0];

                int nr_pokolenia = 0;

                Osobnik maxPopulacji;

                while (nr_pokolenia++ < 1000) // 1000 - zalozona liczba pokolen
                {
                    Osobnik[] nowa_populacja = new Osobnik[populacja.Length];
                    maxPopulacji = nowa_populacja[0];
                    for (int i = 0; i < nowa_populacja.Length; i++) // nowe pokolenie
                    {
                        Osobnik dziecko, mama, tata;
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
                        Osobnik.FunkcjaDopasowania(Osobnik.Fenotyp(populacja[i])),
                        Osobnik.Fenotyp(populacja[i]));
                }

                listaWyników.Add(MaxPopulacji(najlepsiPopulacji.ToArray()));


            }

            Console.WriteLine("Średnia z wszystkich uruchomień: {0}", Średnia(listaWyników.ToArray()));
            Console.ReadKey();
        }

        static Osobnik Selekcja(Osobnik[] populacji)
        {
            Osobnik k1 = populacji[los.Next(populacji.Length)];
            Osobnik k2 = populacji[los.Next(populacji.Length)];

            if (Osobnik.FunkcjaDopasowania(Osobnik.Fenotyp(k1))
                > Osobnik.FunkcjaDopasowania(Osobnik.Fenotyp(k2)))
                return k1;
            else
                return k2;
        }



      

        static Osobnik Rekombinacja(Osobnik mama, Osobnik tata)
        {
            uint maska = ~0u << los.Next(1, 32);
            Osobnik dziecko = new Osobnik();
            dziecko.m_genotyp = maska & mama.m_genotyp
                | ~maska & tata.m_genotyp;

            if (los.NextDouble() < 0.1) // 0.1 - 10% pr. mutacji - zalozenie
            {
                maska = 1u << los.Next(32);
                dziecko.m_genotyp ^= maska;
            }
            return dziecko;
        }

        static Osobnik MaxPopulacji(Osobnik[] populacja)
        {
            Osobnik maxPopulacji = populacja[0];
            foreach (var item in populacja)
            {
                if (Osobnik.FunkcjaDopasowania(item.m_fenotyp) > Osobnik.FunkcjaDopasowania(maxPopulacji.m_fenotyp))
                {
                    maxPopulacji = item;
                }
            }
            return maxPopulacji;
        }

        static double Średnia(Osobnik[] wartosci)
        {
            double suma = 0.0;

            foreach (var item in wartosci)
            {
                suma += Osobnik.FunkcjaDopasowania(item.m_fenotyp);
            }

            return suma / wartosci.Length;
        }

        static bool CzyWZakresie(Osobnik osobnik)
        {
            return osobnik.m_fenotyp > 2.0 || osobnik.m_fenotyp < -2.0;
        }
    }
}
