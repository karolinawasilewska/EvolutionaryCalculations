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
        //public Individual[] Crossover(int[] p1, int[] p2)
        //{
        //    int beginSplit = ENVIRONMENT.random.Next(0, p1.Length - 2);
        //    int endSplit = ENVIRONMENT.random.Next(beginSplit + 1, p1.Length);
        //    int parentSize = p1.Length;
        //    int swathSize = endSplit - beginSplit;
        //    int[] ch1 = new int[parentSize],
        //          ch2 = new int[parentSize],
        //          p1Swath = new int[swathSize],
        //          p2Swath = new int[swathSize];
        //    int j = 0;
        //    for (int i = 0; i < parentSize; i++)
        //    {
        //        if (i >= beginSplit && i < endSplit)
        //        {
        //            p1Swath[j] = p1[i];
        //            p2Swath[j] = p2[i];
        //            j++;
        //        }
        //        ch1[i] = p1[i];
        //        ch2[i] = p2[i];
        //    }

        //    for (int i = 0; i < parentSize; i++)
        //    {
        //        if (i < beginSplit || i >= endSplit)
        //        {
        //            //ch1
        //            if (!Array.Exists(p1Swath, x => x == p2[i]))
        //                ch1[i] = p2[i];
        //            else
        //            {
        //                int toCheck = p2[Array.IndexOf(p1, p2[i])];
        //                while (Array.Exists(p1Swath, x => x == toCheck))
        //                {
        //                    toCheck = p2[Array.IndexOf(p1, toCheck)];
        //                }
        //                ch1[i] = toCheck;
        //            }

        //            //ch2
        //            if (!Array.Exists(p2Swath, x => x == p1[i]))
        //                ch2[i] = p1[i];
        //            else
        //            {
        //                int toCheck = p1[Array.IndexOf(p2, p1[i])];
        //                while (Array.Exists(p2Swath, x => x == toCheck))
        //                {
        //                    toCheck = p1[Array.IndexOf(p2, toCheck)];
        //                }
        //                ch2[i] = toCheck;
        //            }
        //        }
        //    }

        //    Individual child1 = new Individual(prob) { Order = ch1 };
        //    Individual child2 = new Individual(prob) { Order = ch2 };
        //    Individual[] newChildren = new Individual[] { child1, child2 };
        //    return newChildren;
        //}

        public Individual[] Crossover(int[] parent1, int[] parent2)
        {
            Individual child1 = new Individual(prob) { Order = new int[parent1.Length] };
            Individual child2 = new Individual(prob) { Order = new int[parent1.Length] };

            for (int i = 0; i < child1.Order.Length; i++)
            {
                child1.Order[i] = -1;
                child2.Order[i] = -1;
            }


            if (parent1.Length != parent2.Length)
                throw new Exception("Invalid parents");

            int cut1, cut2;

            cut1 = ENVIRONMENT.random.Next(0, parent1.Length - 1);
            cut2 = ENVIRONMENT.random.Next(cut1 + 1, parent1.Length);


            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i >= cut1 && i <= cut2)
                {
                    child1.Order[i] = parent2[i];
                    child2.Order[i] = parent1[i];
                }
            }

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i < cut1 || i > cut2)
                {
                    if (!child1.Order.ContainsInt(parent1[i]))
                    {
                        child1.Order[i] = parent1[i];
                    }
                    else
                    {
                        int currentVal = Array.IndexOf(parent2, parent1[i]);
                        while (child1.Order.ContainsInt(parent1[currentVal]))
                        {
                            currentVal = Array.IndexOf(parent2, parent1[currentVal]);
                        }
                        child1.Order[i] = parent1[currentVal];
                    }
                    if (!child2.Order.ContainsInt(parent2[i]))
                    {
                        child2.Order[i] = parent2[i];
                    }
                    else
                    {
                        int currentVal = Array.IndexOf(parent1, parent2[i]);
                        while (child2.Order.ContainsInt(parent2[currentVal]))
                        {
                            currentVal = Array.IndexOf(parent1, parent2[currentVal]);
                        }
                        child2.Order[i] = parent2[currentVal];
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

        public int[] SwapRandomPoints()
        {
            int p1 = ENVIRONMENT.random.Next(Order.Length);
            int p2 = ENVIRONMENT.random.Next(Order.Length);

            int temp = Order[p1];
            Order[p1] = Order[p2];
            Order[p2] = temp;

            return Order;
        }

        public double TotalDistance { get; set; }
      

        public override string ToString()
        {
            return OrderAsString;
        }

        public void RecalculateTotalDistance()
        {
            TotalDistance = prob.Ocena(Order);
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
                    population[i].Order = population[i].SwapRandomPoints();
                }
                population[i].RecalculateTotalDistance();
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
            public static Individual RouletteSelection(Individual[] population)
            {
                double sum = 0,
                      minValue = 0,
                      randomValue = 0;
                for (int i = 0; i < population.Length; i++)
                {
                    sum += population[i].TotalDistance;
                    if (minValue > population[i].TotalDistance)
                    {
                        minValue = population[i].TotalDistance;
                    }
                }

                sum += -minValue * population.Length;
                randomValue = sum * ENVIRONMENT.random.NextDouble() + minValue * population.Length;
                sum = 0;
                for (int i = 0; i < population.Length; i++)
                {
                    sum += population[i].TotalDistance;
                    if (minValue > population[i].TotalDistance)
                    {
                        minValue = population[i].TotalDistance;
                    }
                }

                return population[(int)(ENVIRONMENT.random.NextDouble() * population.Length)];
            }
            //public static Individual RouletteSelection(Individual[] individuals)
            //{

            //    double weightSum = 0, weightCheck = 0;
            //    for (int i = 0; i < individuals.Length; i++)
            //    {
            //        weightSum += individuals[i].TotalDistance;
            //    }

            //    Individual chosen = individuals[0];
            //    double chosenOne = ENVIRONMENT.random.Next(0, (int)weightSum);

            //    for (int i = 0; i < individuals.Length; i++)
            //    {
            //        weightCheck += individuals[i].TotalDistance;

            //        if (weightCheck > chosenOne)
            //        {
            //            chosen = individuals[i];
            //            break;
            //        }
            //    }
            //    return chosen;
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

                DateTime startTime = DateTime.Now;

                for (int gen = 0; gen < ENVIRONMENT.PopulationCount; gen++)
                {

                    Population newPopulation = new Population(problemKlienta)
                    {
                        Individuals = new Individual[currentPopulation.Individuals.Length]
                    };
                    for (int ind = 0; ind < ENVIRONMENT.PopulationSize; ind += 2)
                    {

                        //wybierz rodziców ruletką
                        //Individual mum = RouletteSelection(currentPopulation.Individuals);
                        //Individual dad = RouletteSelection(currentPopulation.Individuals);
                            Individual mum = Select(currentPopulation.Individuals, 2);
                         Individual dad = Select(currentPopulation.Individuals, 2);




                        if (gen > 200 && gen - bestOne.PopulationIndex > 200)
                        {
                            //mum = bestOne;
                            //criteria.MutationCount = +1000;
                            for (int j = 0; j < 50; j++)
                            {
                                mum.Order= mum.SwapRandomPoints();
                                dad.Order=dad.SwapRandomPoints();
                            }
                            Console.WriteLine("mix");
                        }

                        Individual[] children = new Individual[2];

                        //krzyżowanie
                        switch (criteria.Crossover)
                        {
                            case "PMX":
                                children = pmx.Crossover(mum.Order, dad.Order);
                                break;
                            case "CX":
                                children = cx.Crossover(mum, dad);
                                break;
                            default:
                                break;
                        }


                        //zmutuj
                        for (int i = 0; i < children.Length; i++)
                        {

                            if (ENVIRONMENT.random.NextDouble() < criteria.MutationPossibility)
                            {
                                for (int j = 0; j < criteria.MutationCount; j++)
                                {
                                    children[i].Order=children[i].SwapRandomPoints();
                                }
                            }
                            children[i].RecalculateTotalDistance();
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
                    currentPopulation.Individuals =(Individual[])newPopulation.Individuals.Clone();
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
            public int Rozmiar(int numer_zbioru = 0) { return 30; } // jeśli podany inny niż 0 to zmieniamy na ów zbiór

            public double Ocena(int[] sciezka) // indeksy
            {
                double distance = 0.0;
                City[] cities = new City[sciezka.Length];
                for (int i = 0; i < sciezka.Length; i++)
                {
                    cities[i] = Data.Where(q => q.Index - 1 == sciezka[i]).FirstOrDefault();
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

                    City[] cities = new City[30];

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
