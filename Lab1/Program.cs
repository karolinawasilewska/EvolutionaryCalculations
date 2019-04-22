using System;
using System.Linq;

namespace Ćwiczenie._01
{
    /*Należy zaimplementować algorytm ewolucyjny do zadania znajdowania x, 
     dla którego funkcja f(x)=x* sin(x)* sin(10x) jest maksymalna, 
     przy ograniczeniach: x w przedziale[-2;2] i dokładności obliczeń 10^-9. 
     Implementacja powinna być możliwe prosta, tak aby zdążyć ją wykonać w czasie zajęć.
     Przykładowo, można założyć że: rozmiar populacji będzie stały i wynosił 20 osobników, 
     kryterium stopu 1000 pokoleń, selekcja turniejowa, krzyżowanie jednopunktowe.*/

    internal class Osobnik // Rozwiązanie problemu jakiegoś
    {
        private const double _dokładność = 0.000000001; // W zadaniu była dokładność 10
        public const float szansaNaMutacje = 0.15f; // bo czemu nie + często tak się pisze
        public const int MaxLiczbaMutowanychGenów = 8; // bo czemu nie

        public uint Genotyp; // Na tym wykonujemy krzyżowanie + mutacje

        public double Fenotyp
        {
            get { return -2.0 + Genotyp * _dokładność; } // [-2; 2], 1*10^-9 => 4mld możliwych rozwiązań
        } // To wykorzystujemy do funkcji (oceny)

        public bool ŚmiertelnieZmutowany()
        {
            return Fenotyp > 2.0 || Fenotyp < -2.0;
        }

        public void Mutacja()
        {
            Random rng = new Random();
            for(int i = 0; i < MaxLiczbaMutowanychGenów; i++)
            {
                if(rng.NextDouble() < szansaNaMutacje) // NextDouble -> [0.0; 1.0)
                {
                    // Jednak mutacja
                    int gdzieMutacja = rng.Next(32);
                    uint maska = (uint)Math.Pow(2, gdzieMutacja); // np. 0 0 0 1 0 
                    Genotyp = Genotyp ^ maska; // XOR 
                }
            }
        }

        public static Osobnik Krzyżowanie(Osobnik mama, Osobnik tata)
        {
            Random rng = new Random();
            // Tata = 0 1 0 | 1 0 1 0 1
            // Mama = 0 1 0 | 0 1 1 0 1

            // Dzi1 = 0 1 0 | 1 0 1 0 1
            // Dzi2 = 0 1 0 | 0 1 1 0 1

            int punktPodziału = rng.Next(1, 31); // od 0 do liczby bitów genotypu (tutaj 32) 
                                                 //od 1, żeby nie wziąć któregoś z rodziców w całości


            uint maska = uint.MaxValue; // 11111111   ~0u (same jedynki)
            maska = maska << punktPodziału;
                                            // dla punktPodziału=2:
                                            // 11111100
            // Dzi1 = Tata & maska
            // 0 1 0 | 1 0 1 0 1
            // 1 1 1 | 0 0 0 0 0
            // 0 1 0 | 0 0 0 0 0

            // Dzi1 += Mama & !maska
            // 0 1 0 | 0 1 1 0 1
            // 0 0 0 | 1 1 1 1 1
            // 0 0 0 | 0 1 1 0 1

            // 0 1 0 | 0 1 1 0 1

            Osobnik dziecko = new Osobnik()
            {
                Genotyp = (tata.Genotyp & maska) | (mama.Genotyp & ~maska)
            };

            return dziecko;
        }

    }

    internal class Populacja
    {
        public const int RozmiarPopulacji = 20;
        public const int RozmiarTurnieju = 3; // tak między 10-20% rozmiaru populacji

        public Osobnik[] Osobniki = new Osobnik[RozmiarPopulacji];
        public static Osobnik NajlepszyWHistorii = null;
        
        public Osobnik NajlepszyWPopulacji()
        {
            Osobnik najlepszy = Osobniki[0];
            for(int i = 1; i < RozmiarPopulacji; i++)
                najlepszy = Środowisko.Lepszy(najlepszy, Osobniki[i]);

            NajlepszyWHistorii = Środowisko.Lepszy(najlepszy, NajlepszyWHistorii);

            return najlepszy;
        }

        public void GenerujPierwsząPopulację()
        {
            Random rng = new Random();
            for (int i = 0; i < RozmiarPopulacji; i++)
            {
                // LOSOWO
                //Osobniki[i] = new Osobnik()
                //{
                //    Genotyp = (uint) (0 | rng.Next(int.MaxValue))
                //};
                do
                {
                    Osobniki[i] = new Osobnik()
                    {
                        Genotyp = (uint) i * (UInt32.MaxValue / RozmiarPopulacji) + (uint)rng.Next() //Populacja równomiernie rozłożona + spora losowość
                    };
                } while(Osobniki[i].ŚmiertelnieZmutowany()); // Niektóre osobniki mogą być niedopuszczalne jako rozwiązania
            }

            NajlepszyWHistorii = NajlepszyWPopulacji();
        }

        public void GenerujNowąPopulację_Turniej()
        {
            Populacja nowePokolenie = new Populacja();
            for(int i = 0; i < RozmiarPopulacji; i++)
            {
                do
                {
                    Osobnik mama = Selekcja_Turniej();
                    Osobnik tata = Selekcja_Turniej();

                    nowePokolenie.Osobniki[i] = Osobnik.Krzyżowanie(mama, tata);
                    nowePokolenie.Osobniki[i].Mutacja();
                } while(nowePokolenie.Osobniki[i].ŚmiertelnieZmutowany()); // Niektóre osobniki mogą być niedopuszczalne do rozwiązania
            }

            NajlepszyWHistorii = Środowisko.Lepszy(NajlepszyWHistorii, nowePokolenie.NajlepszyWPopulacji());
        }

        public Osobnik Selekcja_Turniej()
        {
            Random rng = new Random();
            Osobnik[] turniej = new Osobnik[RozmiarTurnieju];
            for (int i = 0; i < RozmiarTurnieju; i++)
                turniej[i] = Osobniki[rng.Next(RozmiarPopulacji)];
            return turniej.OrderByDescending(x => Środowisko.Funkcja(x.Fenotyp)).First(); // zwraca najlepszego z turnieju`
        }

    }

    internal class Środowisko
    {
        public Populacja Populacja;

        //f(x)=x* sin(x)*sin(10x)
        public static double Funkcja(double fenotyp)
        {
            return fenotyp * Math.Sin(fenotyp) * Math.Sin(10 * fenotyp);
        }

        public static Osobnik Lepszy(Osobnik x1, Osobnik x2)
        {
            //if (Funkcja(x1.Fenotyp) > Funkcja(x2.Fenotyp))
            //{
            //    return x1;
            //}
            //else return x2;
            if(x1 == null)
                return x2;
            if(x2 == null)
                return x1;
            Osobnik lepszy = Funkcja(x1.Fenotyp) > Funkcja(x2.Fenotyp) ? x1 : x2;
            return lepszy;
        }

        public bool WarunekStopu()
        {
            // np. W zadaniu szukamy maksymalnej wartości funkcji, przynajmniej większej niż X
            // Jeżeli Funkcja(NajlepszyWHistorii.Fenotyp) > X, to możemy się zatrzymać

            // Ponieważ nie mamy określonej minimalnej wartości, to kręcimy się w pętli w Main
            // Zatem tutaj return true;

            return true;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Środowisko srodowisko = new Środowisko();
            srodowisko.Populacja = new Populacja();
            srodowisko.Populacja.GenerujPierwsząPopulację();

            // Tutaj mamy się kręcić w pętli przez 1000 pokoleń tak czy tak, więc wywalone
            //if (srodowisko.WarunekStopu())
            //    Stop();

            for(int i = 0; i < 1000; i++)
            {
                if(i % 50 == 0)
                    Console.WriteLine("x=" + Populacja.NajlepszyWHistorii.Fenotyp + ", f(x)=" + Środowisko.Funkcja(Populacja.NajlepszyWHistorii.Fenotyp));

                srodowisko.Populacja.GenerujNowąPopulację_Turniej();
            }
            Osobnik x = Populacja.NajlepszyWHistorii;
            Console.WriteLine("x=" + x.Fenotyp + ", f(x)=" + Środowisko.Funkcja(x.Fenotyp));

            Console.ReadKey();
        }
    }
}
