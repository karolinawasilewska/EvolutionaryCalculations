using System;
using System.IO;
using System.Linq;
/*
* ruletka 
* pmx cx 
* mutacja przez zamianę dwóch miast
* ruletka
* https://pastebin.com/QEAucmnh
* 
*/
namespace KarolinaWasilewska
{
    public static class ENVIRONMENT
    {
        public static bool ContainsInt(this int[] array, int element)
        {
            foreach (var item in array)
            {
                if (element == item)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Liczba populacji (dowolna)
        /// </summary>
        public static int PopulationCount { get; set; }
        /// <summary>
        /// Liczba miast w ramach jednego osobnika (musi być zgodna z liczbą danych w pliku)
        /// </summary>
        public static int IndividualSize { get; set; }
        /// <summary>
        /// liczba osobników w populacji
        /// </summary>
        public static int PopulationSize { get; set; }
        public static Random random { get; set; } = new Random();
    }

    public class PMX
    {
        public srodowisko.ProblemKlienta prob = null;

        public PMX(srodowisko.ProblemKlienta ProblemKlienta)
        {
            prob = ProblemKlienta;
        }


        public Individual[] Crossover(Individual parent1, Individual parent2)
        {
            Individual child1 = new Individual(prob) { Order = new int[parent1.Order.Length] };
            Individual child2 = new Individual(prob) { Order = new int[parent1.Order.Length] };

            for (int i = 0; i < child1.Order.Length; i++)
            {
                child1.Order[i] = -1;
                child2.Order[i] = -1;
            }


            if (parent1.Order.Length != parent2.Order.Length)
                throw new Exception("Invalid parents");

            int cut1, cut2;

            cut1 = ENVIRONMENT.random.Next(0, parent1.Order.Length - 1);
            cut2 = ENVIRONMENT.random.Next(cut1 + 1, parent1.Order.Length);


            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i >= cut1 && i <= cut2)
                {
                    child1.Order[i] = parent2.Order[i];
                    child2.Order[i] = parent1.Order[i];
                }
            }

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i < cut1 || i > cut2)
                {
                    if (!child1.Order.ContainsInt(parent1.Order[i]))
                    {
                        child1.Order[i] = parent1.Order[i];
                    }
                    else
                    {
                        int currentVal = Array.IndexOf(parent2.Order, parent1.Order[i]);
                        while (child1.Order.ContainsInt(parent1.Order[currentVal]))
                        {
                            currentVal = Array.IndexOf(parent2.Order, parent1.Order[currentVal]);
                        }
                        child1.Order[i] = parent1.Order[currentVal];
                        //if (!child1.Order.ContainsInt(parent1.Order[i]))
                         //   child1.Order[i] = child2.Order[Array.IndexOf(child1.Order, parent1.Order[i])];
                    }
                    if (!child2.Order.ContainsInt(parent2.Order[i]))
                    {
                        child2.Order[i] = parent2.Order[i];
                    }
                    else
                    {
                        int currentVal = Array.IndexOf(parent1.Order, parent2.Order[i]);
                        while (child2.Order.ContainsInt(parent2.Order[currentVal]))
                        {
                            currentVal = Array.IndexOf(parent1.Order, parent2.Order[currentVal]);
                        }
                        child2.Order[i] = parent2.Order[currentVal];
                    }
                }
            }

            return new Individual[] { child1, child2 };
        }
    }
    public class CX
    {
        public srodowisko.ProblemKlienta prob = null;

        public CX(srodowisko.ProblemKlienta ProblemKlienta)
        {
            prob = ProblemKlienta;
        }

        public Individual[] Crossover(Individual parent1, Individual parent2)
        {

            Individual child1 = new Individual(prob);
            Individual child2 = new Individual(prob);

            for (int i = 0; i < child1.Order.Length; i++)
            {
                child1.Order[i] = -1;
                child2.Order[i] = -1;
            }

            child1.Order[0] = parent1.Order[0];
            child2.Order[0] = parent2.Order[0];

            int currentIndex = 0;
            while (!child1.Order.ContainsInt(parent2.Order[currentIndex]))
            {
                int test = parent2.Order[currentIndex];
                int index = Array.IndexOf(parent1.Order, test);
                child1.Order[index] = test;
                currentIndex = index;
            }
            currentIndex = 0;
            while (!child2.Order.ContainsInt(parent1.Order[currentIndex]))
            {
                int test = parent1.Order[currentIndex];
                int index = Array.IndexOf(parent2.Order, test);
                child2.Order[index] = test;
                currentIndex = index;
            }

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (child1.Order[i] == -1)
                    child1.Order[i] = parent2.Order[i];
                if (child2.Order[i] == -1)
                    child2.Order[i] = parent1.Order[i];
            }


            return new Individual[] { child1, child2 };
        }
    }

    public class Individual
    {
        public srodowisko.ProblemKlienta prob = null;

        public Individual(srodowisko.ProblemKlienta ProblemKlienta)
        {
            prob = ProblemKlienta;
        }
        public int[] Order { get; set; } = new int[ENVIRONMENT.IndividualSize];

        public string OrderAsString
        {
            get
            {
                string order = "Order: ";
                foreach (var item in Order)
                {
                    order += item + ",";
                }
                return order;
            }
        }
        public int PopulationIndex { get; set; }

        public static int[] SwapRandomPoints(int[] tab)
        {
            int p1 = ENVIRONMENT.random.Next(tab.Length);
            int p2 = ENVIRONMENT.random.Next(tab.Length);

            int temp = tab[p1];
            tab[p1] = tab[p2];
            tab[p2] = temp;

            return tab;
        }

        public double TotalDistance
        {
            get
            {
                return prob.Ocena(Order);
            }
            set { }
        }

        public override string ToString()
        {
            return OrderAsString;
        }

    }
    public class Population
    {
        public srodowisko.ProblemKlienta prob = null;

        public Population(srodowisko.ProblemKlienta ProblemKlienta)
        {
            prob = ProblemKlienta;
        }
        public Individual[] Individuals { get; set; }


        public Individual[] GetRandomPopulation()
        {
            Individual[] population = new Individual[ENVIRONMENT.PopulationSize];


            for (int i = 0; i < population.Length; i++)
            {
                //stwórz osobnika
                population[i] = new Individual(prob);

                for (int j = 0; j < ENVIRONMENT.IndividualSize; j++)
                {
                    population[i].Order[j] = j;
                }

                //posortuj go (tyle swapów ile miast)
                for (int j = 0; j < ENVIRONMENT.IndividualSize; j++)
                {
                    population[i].Order = Individual.SwapRandomPoints(population[i].Order);
                }
            }

            return population;

        }
    }
    public class Criteria
    {
        public enum StopCriterias
        {
            GenerationCount = 1,
            Time = 2
        }
        public string Crossover { get; set; }
        public StopCriterias? StopCriteria { get; set; }
        public int PopulationSize { get; set; }
        public int? GenerationCount { get; set; }
        public TimeSpan? Time { get; set; }
        public DateTime FinishTime { get; set; }
        public short DatasetNumber { get; set; }
        public double MutationPossibility { get; set; }
        public short MutationCount { get; set; }

        public override string ToString()
        {
            return string.Format("Stop criteria: {1}\nPopulation size: {3}\n" +
                "Generation count: {4}\nTime: {5}", StopCriteria.ToString(),
                PopulationSize, GenerationCount, Time);
        }
        class Program
        {
            protected static srodowisko.ProblemKlienta problemKlienta = new srodowisko.ProblemKlienta();

            public static Individual RouletteSelection(Individual[] individuals)
            {
              
                double weightSum = 0, weightCheck = 0;
                for (int i = 0; i < individuals.Length; i++)
                {
                    weightSum += individuals[i].TotalDistance;
                }

                Individual chosen = individuals[0];
                double chosenOne = ENVIRONMENT.random.Next(0, (int)weightSum);

                for (int i = 0; i < individuals.Length; i++)
                {
                    weightCheck += individuals[i].TotalDistance;

                    if (weightCheck > chosenOne)
                    {
                        chosen = individuals[i];
                        break;
                    }
                }
                return chosen;
            }

            //public static Individual RouletteSelection(Individual[] individuals)
            //{
            //    // Individual selected = new Individual();
            //    double sum = 0;
            //    for (int i = 0; i < individuals.Length; i++)
            //    {
            //        sum += (100000.0 / problemKlienta.Ocena(individuals[i].Order));
            //    }
            //   // Console.WriteLine("Ruletka suma: {0}", sum);
            //    double los = ENVIRONMENT.random.NextDouble() * sum;
            //    Individual ind = individuals[0];
            //    double currentSum = 0;
            //    for (int j = 0; j < individuals.Length; j++)
            //    {
            //        currentSum += 100000.0 / problemKlienta.Ocena(individuals[j].Order);
            //        if (currentSum >= los)
            //        {
            //            ind = individuals[j];
            //         //   Console.WriteLine("Roulette wybrano dystans: {0}", ind.TotalDistance);
            //            break;
            //        }
            //    }
            // //   Console.WriteLine("Roulette wybrano dystans: {0}", ind.TotalDistance);
            //    return ind;
            //}
            public static Individual Select(Individual[] individuals, int size)
            {
                Individual[] contestans = new Individual[size];

                for (int i = 0; i < contestans.Length; i++)
                {
                    contestans[i] = individuals[ENVIRONMENT.random.Next(individuals.Length - 1)];
                }

                Individual best = individuals[0];
                double minDistance = problemKlienta.Ocena(contestans[0].Order);
                for (int i = 1; i < contestans.Length; i++)
                {
                    double currentDistance = problemKlienta.Ocena(contestans[i].Order);
                    if (minDistance > currentDistance)
                    {
                        minDistance = currentDistance;
                        best = contestans[i];
                    }
                }
                return best;
            }
            static void Main(string[] args)
            {
                Criteria criteria = new Criteria()
                {
                    FinishTime = DateTime.Parse(args[0]),
                    Crossover = args[1],
                    StopCriteria = (StopCriterias)int.Parse(args[2]),
                    PopulationSize = int.Parse(args[3]),
                    GenerationCount = int.Parse(args[4]),
                    Time = TimeSpan.Parse(args[5]),
                    MutationPossibility = double.Parse(args[6]),
                    MutationCount = short.Parse(args[7]),
                    DatasetNumber = short.Parse(args[8])
                };

                ENVIRONMENT.IndividualSize = problemKlienta.Rozmiar(criteria.DatasetNumber);
                ENVIRONMENT.PopulationSize = criteria.PopulationSize;
                ENVIRONMENT.PopulationCount = criteria.GenerationCount ?? 0;

                Individual bestOne = new Individual(problemKlienta);

                PMX pmx = new PMX(problemKlienta);
                CX cx = new CX(problemKlienta);
                Population currentPopulation = new Population(problemKlienta);
                currentPopulation.Individuals = currentPopulation.GetRandomPopulation();
                bestOne = currentPopulation.Individuals[0];
                //---start!---
                for (int gen = 0; gen < ENVIRONMENT.PopulationCount; gen++)
                {
                    Population newPopulation = new Population(problemKlienta)
                    {
                        Individuals = new Individual[currentPopulation.Individuals.Length]
                    };
                    for (int ind = 0; ind < ENVIRONMENT.PopulationSize; ind += 2)
                    {

                        //wybierz rodziców ruletką
                     //   Individual mum = RouletteSelection(currentPopulation.Individuals);
                     //   Individual dad = RouletteSelection(currentPopulation.Individuals);
                         Individual mum = Select(currentPopulation.Individuals, 2);
                        Individual dad = Select(currentPopulation.Individuals, 2);




                        if (gen > 50 && gen- bestOne.PopulationIndex >50)
                        {
                            mum = bestOne;
                            criteria.MutationCount = +1000;
                         //   Console.WriteLine("mix");
                        }

                        Individual[] children = new Individual[2];

                        //krzyżowanie
                        switch (criteria.Crossover)
                        {
                            case "PMX":
                                children = pmx.Crossover(mum, dad);
                                break;
                            case "CX":
                                children = cx.Crossover(mum, dad);
                                break;
                            default:
                                break;
                        }


                        //zmutuj
                        //foreach (var item in children)
                        for (int i = 0; i < children.Length; i++)
                        {

                            if (ENVIRONMENT.random.NextDouble() < criteria.MutationPossibility)
                            {
                                for (int j = 0; j < criteria.MutationCount; j++)
                                {
                                    Individual.SwapRandomPoints(children[i].Order);
                                }
                            }

                            newPopulation.Individuals[ind + i] = children[i];
                            if (bestOne.TotalDistance > children[i].TotalDistance)
                            {
                                bestOne = children[i];
                                bestOne.PopulationIndex = gen;
                                if (criteria.MutationCount > int.Parse(args[7]))
                                    criteria.MutationCount -= 999;
                                Console.WriteLine("{0} dla populacji {1}", (int)bestOne.TotalDistance, gen);
                            }


                        }
                    }
                    currentPopulation = newPopulation;
                }
                Console.WriteLine("Finish!");
                Console.ReadKey();
            }
        }
    }

    namespace srodowisko
    {

        public class City
        {
            public double Long { get; set; }
            public double Lat { get; set; }
            public int Index { get; set; }

            public static double DistanceToIt(City city1, City city2)
            {
                double distance =
                               Math.Sqrt(
                                   Math.Pow(city1.Lat - city2.Lat, 2) +
                                   Math.Pow(city1.Long - city2.Long, 2));
                return distance;
            }
        }
        public class ProblemKlienta
        {
            public ProblemKlienta()
            {
                DataReader.ReadData();
            }
            public int Rozmiar(int numer_zbioru = 0) { return 980; } // jeśli podany inny niż 0 to zmieniamy na ów zbiór

            public double Ocena(int[] sciezka) // indeksy
            {
                double distance = 0.0;
                City[] cities = new City[sciezka.Length];
                for (int i = 0; i < sciezka.Length; i++)
                {
                    cities[i] = Data.Where(q => q.Index-1 == sciezka[i]).FirstOrDefault();
                }

                for (int i = 1; i < cities.Length; i++)
                {
                    distance += City.DistanceToIt(cities[i - 1], cities[i]);
                }

                distance += City.DistanceToIt(cities[cities.Length - 1], cities[0]);
                return distance;
            }
            //public int Rozmiar(int numer_zbioru = 0)// jeśli podany inny niż 0 to zmieniamy na ów zbiór
            //{
            //    return 10;
            //}
            public static City[] Data { get; set; }

            //public double Ocena(int[] sciezka) // indeksy
            //{
            //    double distance = 0.0;
            //    City[] cities = new City[sciezka.Length];
            //    for (int i = 0; i < sciezka.Length - 1; i++)
            //    {
            //        cities[i] = Data.Where(q => q.Index == sciezka[i]).FirstOrDefault();
            //    }

            //    for (int i = 1; i < cities.Length; i++)
            //    {
            //        distance += City.DistanceToIt(cities[i - 1], cities[i]);
            //    }

            //    distance += City.DistanceToIt(cities[cities.Length - 1], cities[0]);
            //    return distance;
            //}
            public class DataReader
            {
                static public void ReadData()
                {

                    City[] cities = new City[980];

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../data.txt");
                    int index = 0;
                    using (var fileStream = File.OpenRead(path))
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string[] lineArray = line.Split(' ');
                            cities[index] = new City()
                            {
                                Index = int.Parse(lineArray[0]),
                                Long = double.Parse(lineArray[1].Replace('.', ',')),
                                Lat = double.Parse(lineArray[2].Replace('.', ',')),
                            };
                            index++;
                        }
                    }
                    Data = cities;
                }
            }
        }
    }
}
