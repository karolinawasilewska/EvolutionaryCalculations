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
                int parentVal = parent2.Order[currentIndex];
                int index = Array.IndexOf(parent1.Order, parentVal);
                child1.Order[index] = parentVal;
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

            //Array.Sort(child2.Order);
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

        public Individual Best { get; set; }
        public Individual Worst { get; set; }

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

                int swapCount = ENVIRONMENT.IndividualSize > 1000 ? ENVIRONMENT.IndividualSize / 5 : ENVIRONMENT.IndividualSize;
                for (int j = 0; j < swapCount ; j++)
                {
                    population[i].Order = population[i].SwapRandomPoints();
                }

                if (i == 0)
                {
                    Best = population[0];
                    Worst = population[0];
                }
                population[i].RecalculateTotalDistance();
                if (Best.TotalDistance > population[i].TotalDistance)
                    Best = population[i];
                if (Worst.TotalDistance < population[i].TotalDistance)
                    Worst = population[i];
            }
            Console.WriteLine("B/W: {0}/{1}", Best.TotalDistance, Worst.TotalDistance);
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
        public StopCriterias? StopCriterion { get; set; }
        public int PopulationSize { get; set; }
        public int GenerationCount { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime FinishTime { get; set; }
        public short DatasetNumber { get; set; }
        public double MutationPossibility { get; set; }
        public short MutationCount { get; set; }
        public short StagnationMoment { get; set; }
        public short StagnationMutationCount { get; set; }

        public override string ToString()
        {
            return string.Format("Crossover: {0}, Stop criterium: {1}, Population size: {2}, " +
                "Generation count: {3}, Time: {4}, Finish time: {5}, Dataset number: {6}, " +
                "Mutation possibility: {7}, Mutation count: {8}, Stagnation moment: {9}, " +
                "Stagnation mutation count: {10}", Crossover, StopCriterion, PopulationSize, GenerationCount, Time, FinishTime, 
               DatasetNumber, MutationPossibility, MutationCount, StagnationMoment, StagnationMutationCount);
        }
        class Program
        {
            protected static srodowisko.ProblemKlienta problemKlienta = new srodowisko.ProblemKlienta();
            public static Individual RouletteSelection(Population individuals)
            {
               
                  //  var sorted = individuals.genotypes.OrderBySurvivalScore();
                    var theLastIndividual = individuals.Worst;
                    double maxSumRange = 0;
                    for (int j = 0; j < individuals.Individuals.Length; j++)
                    {
                        maxSumRange += theLastIndividual.TotalDistance / individuals.Individuals[j].TotalDistance;
                    }
                    Individual parent = null;
                    var i = 0;
                    var sum = theLastIndividual.TotalDistance / individuals.Individuals[i].TotalDistance;
                    var randomScoreValue = ENVIRONMENT.random.NextDouble() * maxSumRange;
                    while (parent == null)
                    {
                        if (randomScoreValue <= sum)
                        {
                            parent = individuals.Individuals[i];
                            break;
                        }
                        i++;
                        sum += theLastIndividual.TotalDistance / individuals.Individuals[i].TotalDistance;
                    }
                    if (parent == null)
                    {
                        parent = individuals.Individuals[individuals.Individuals.Length - 1];
                    }
                    return parent;
               
                //double sum = 0;
                //for (int k = 0; k < individuals.Length; k++)
                //{
                //    sum += 1 / individuals[k].TotalDistance;
                //}
                //Individual selected = new Individual(problemKlienta);

                //double los = ENVIRONMENT.random.NextDouble() * sum;

                //Individual ind = individuals[0];
                //double currentSum = 0;
                //for (int i = 0; i < individuals.Length; i++)
                //{
                //    currentSum += 1 / individuals[i].TotalDistance;
                //    if (currentSum >= los)
                //    {
                //        return individuals[i];
                //    }
                //}
                //return selected;
            }

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
                        return contestans[i];
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
                    StopCriterion = (StopCriterias)int.Parse(args[2]),
                    PopulationSize = int.Parse(args[3]),
                    GenerationCount = int.Parse(args[4]),
                    Time = args[5] == "0" ? TimeSpan.Zero : TimeSpan.FromMinutes(double.Parse(args[5])),
                    MutationPossibility = double.Parse(args[6]),
                    MutationCount = short.Parse(args[7]),
                    StagnationMoment = short.Parse(args[8]),
                    StagnationMutationCount = short.Parse(args[9]),
                    DatasetNumber = short.Parse(args[10])
                };
                ENVIRONMENT.IndividualSize = problemKlienta.Rozmiar(criteria.DatasetNumber);
                ENVIRONMENT.PopulationSize = criteria.PopulationSize;
                ENVIRONMENT.PopulationCount = criteria.StopCriterion == StopCriterias.GenerationCount ? criteria.GenerationCount : int.MaxValue;

                Console.WriteLine("Started at {0}, Individual size: {1}, {2} ", DateTime.Now, ENVIRONMENT.IndividualSize, criteria.ToString());

                Individual bestOne = new Individual(problemKlienta);
                Individual bestInPopulation = new Individual(problemKlienta);
                Individual worstInPopulation = new Individual(problemKlienta);


                PMX pmx = new PMX(problemKlienta);
                CX cx = new CX(problemKlienta);
                Population currentPopulation = new Population(problemKlienta);
                currentPopulation.Individuals = currentPopulation.GetRandomPopulation();
                bestOne = currentPopulation.Individuals[0];
                //---start!---

                DateTime startTime = DateTime.Now;

                for (int gen = 0; gen < ENVIRONMENT.PopulationCount; gen++)
                {
                    if (DateTime.Now < criteria.FinishTime)
                    {
                        if (criteria.Time==TimeSpan.Zero || ((DateTime.Now - startTime).TotalMinutes < criteria.Time.TotalMinutes))
                        {
                            Population newPopulation = new Population(problemKlienta)
                            {
                                Individuals = new Individual[currentPopulation.Individuals.Length]
                            };
                            for (int ind = 0; ind < ENVIRONMENT.PopulationSize; ind += 2)
                            {
                                if (DateTime.Now < criteria.FinishTime)
                                {
                                    if (criteria.Time == TimeSpan.Zero || ((DateTime.Now - startTime).TotalMinutes < criteria.Time.TotalMinutes))
                                    {
                                        //wybierz rodziców ruletką
                                        Individual mum = RouletteSelection(currentPopulation);
                                        Individual dad = RouletteSelection(currentPopulation);

                                        if (gen > criteria.StagnationMoment && gen - bestOne.PopulationIndex > criteria.StagnationMoment)
                                        {
                                            mum = bestOne;
                                            for (int j = 0; j < criteria.StagnationMutationCount; j++)
                                            {
                                                dad.Order = dad.SwapRandomPoints();
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



                                        for (int i = 0; i < children.Length; i++)
                                        {

                                            if (ENVIRONMENT.random.NextDouble() < criteria.MutationPossibility)
                                            {  //zmutuj
                                                for (int j = 0; j < criteria.MutationCount; j++)
                                                {
                                                    children[i].Order = children[i].SwapRandomPoints();
                                                }
                                            }
                                            children[i].RecalculateTotalDistance();

                                            //zastąp najgorszego nowej populacji, najlepszym ze starej

                                            if (ind == 0 && i == 0)
                                            {
                                                newPopulation.Individuals[ind] = children[i];
                                                newPopulation.Best = newPopulation.Individuals[ind];
                                                newPopulation.Worst = newPopulation.Individuals[ind];
                                            }

                                            if (i % 2 == 0 && !(ind == 0 && i == 0))
                                            {
                                                newPopulation.Individuals[ind] = children[i];

                                                if (newPopulation.Best.TotalDistance > newPopulation.Individuals[ind].TotalDistance)
                                                    newPopulation.Best = newPopulation.Individuals[ind];
                                                if (newPopulation.Worst.TotalDistance < newPopulation.Individuals[ind].TotalDistance)
                                                    newPopulation.Worst = newPopulation.Individuals[ind];                                                
                                            }
                                            else if (!(ind == 0 && i == 0))
                                            {
                                                newPopulation.Individuals[ind + 1] = children[i];
                                                if (newPopulation.Best.TotalDistance > newPopulation.Individuals[ind + 1].TotalDistance)
                                                    newPopulation.Best = newPopulation.Individuals[ind + 1];
                                                if (newPopulation.Worst.TotalDistance < newPopulation.Individuals[ind + 1].TotalDistance)
                                                    newPopulation.Worst = newPopulation.Individuals[ind + 1];
                                            }

                                           

                                            if (bestOne.TotalDistance > children[i].TotalDistance)
                                            {
                                                bestOne = children[i];
                                                bestOne.PopulationIndex = gen;

                                                Console.WriteLine("BestOne {0} pop {1}", (int)bestOne.TotalDistance, gen);
                                            }
                                        }
                                    }
                                }
                            }
                            //       Console.WriteLine("{0}/{1}", newPopulation.Best.TotalDistance, newPopulation.Worst.TotalDistance);
                            Console.WriteLine(newPopulation.Best.TotalDistance);
                            //if (newPopulation.Worst.TotalDistance > currentPopulation.Best.TotalDistance)
                            //{
                                int index = Array.IndexOf(newPopulation.Individuals, newPopulation.Worst);
                            newPopulation.Individuals[index] = bestOne;
                            //    newPopulation.Individuals[index] = currentPopulation.Best;
                            //}

                            currentPopulation.Individuals = (Individual[])newPopulation.Individuals.Clone();
                        }
                        else { break; }
                    }
                    else { break; }
                }
                Console.WriteLine("Finish! Time: {0}, BestVal: {1}, BVPop: {2}", DateTime.Now, bestOne.TotalDistance, bestOne.PopulationIndex);
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

            public static double DistanceToIt(City city1, City _point)
            {
                return Math.Sqrt(Math.Pow(city1.Long - _point.Long, 2) + Math.Pow(city1.Lat - _point.Lat, 2));
            }
        }
        public class ProblemKlienta
        {
            public ProblemKlienta()
            {
                DataReader.ReadData();
            }
            public int Rozmiar(int numer_zbioru = 0) { return 29; } // jeśli podany inny niż 0 to zmieniamy na ów zbiór

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

                    City[] cities = new City[29];

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
