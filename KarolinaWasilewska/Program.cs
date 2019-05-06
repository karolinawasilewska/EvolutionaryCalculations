using System;
/*
 * ruletka 
 * pmx cx 
 * mutacja przez zamianę dwóch miast
 * 
 */
namespace KarolinaWasilewska
{
    public static class ENVIRONMENT
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

        public static bool Contains(this City[] array, City element)
        {
            foreach (var item in array)
            {
                if (element == item)
                    return true;
            }
            return false;
        }

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
        public const int PopulationCount = 1000;
        /// <summary>
        /// Liczba miast w ramach jednego osobnika (musi być zgodna z liczbą danych w pliku)
        /// </summary>
        public const int IndividualSize = 100;
        /// <summary>
        /// liczba osobników w populacji
        /// </summary>
        public const int PopulationSize = 10;
        /// <summary>
        /// Lista miast z pliku
        /// </summary>
        public static City[] cities
        {
            get;
            set;
        }
        public static Random random { get; set; } = new Random();
    }
    public class DataReader
    {
        static public City[] ReadData()
        {

            City[] cities = new City[ENVIRONMENT.IndividualSize];

            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../data.txt");
            //int index = 0;
            //using (var fileStream = File.OpenRead(path))
            //using (var streamReader = new StreamReader(fileStream))
            //{
            //    String line;
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        string[] lineArray = line.Split(' ');
            //        cities[index] = new City()
            //        {
            //            Index = int.Parse(lineArray[0]),
            //            Longitude = double.Parse(lineArray[1].Replace('.', ',')),
            //            Latitude = double.Parse(lineArray[2].Replace('.', ',')),
            //        };
            //        index++;
            //    }
            //}
            return cities;
        }
    }
    public class DistanceHelper
    {
        public static double FindDistance(City city1, City city2)
        {
            double distance =
                Math.Sqrt(
                    Math.Pow(city1.Latitude - city2.Latitude, 2) +
                    Math.Pow(city1.Longitude - city2.Longitude, 2)); ;
            return distance;
        }

        public static double CountDistance(City[] cities)
        {
            double distance = 0;

            for (int i = 1; i < cities.Length; i++)
            {
                distance += FindDistance(cities[i - 1], cities[i]);
            }

            distance += FindDistance(cities[cities.Length - 1], cities[0]);

            return distance;

        }

    }
    public class City
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Index { get; set; }

        public static City GetRandomCity(int currentlyPossibleSize)
        {
            int index = ENVIRONMENT.random.Next(currentlyPossibleSize);
            return ENVIRONMENT.cities[index];
        }

        public override string ToString()
        {
            return string.Format("Index: {0}, X: {1}, Y: {2}", Index, Longitude, Latitude);
        }
    }
    public class Individual
    {
        public City[] Cities { get; set; } = new City[ENVIRONMENT.IndividualSize];
        public int[] Order { get; set; } = new int[ENVIRONMENT.IndividualSize];

        public string OrderAsString
        {
            get
            {
                string order = "";
                foreach (var item in Order)
                {
                    order += item + ",";
                }
                return order;
            }
        }

        //{
        //    get
        //    {
        //        for (int i = 0; i < Cities.Length; i++)
        //        {
        //            Order[i] = Cities[i].Index;
        //        }
        //        return Order;
        //    }
        //    set { }
        //}
        public City[] RemainingCities { get; set; }

        public double TotalDistance { get; set; } = 0;

        public void CreateOrder()
        {
            for (int i = 0; i < Cities.Length; i++)
            {
                Order[i] = Cities[i].Index;
            }
        }

        public City GetRandomCity(int currentlyPossibleSize)
        {
            int index = ENVIRONMENT.random.Next(currentlyPossibleSize);
            return RemainingCities[index];
        }
        public void RemoveFromPossible(City[] remainingCities, City randomCity)
        {
            int randomCityIndex = remainingCities.IndexOf(randomCity);
            RemainingCities = remainingCities;
            for (int i = randomCityIndex; i < remainingCities.Length; i++)
            {
                if (i == remainingCities.Length - 1)
                    RemainingCities[i] = null;
                else
                    RemainingCities[i] = remainingCities[i + 1];
            }

            Array.Resize(ref remainingCities, remainingCities.Length - 1);
            RemainingCities = remainingCities;
        }

        public void SetTotalDistance()
        {
            TotalDistance = DistanceHelper.CountDistance(Cities);
        }

        public override string ToString()
        {
            string text = string.Format("Total distance: {0}; Miasta: ", TotalDistance);
            foreach (var item in Cities)
            {
                text = string.Format("{0}, {1}", text, item.Index);
            }
            return text;
        }

    }
    public class Population
    {
        public Individual[] Individuals { get; set; }


        public static Individual[] GetRandomPopulation()
        {
            Individual[] population = new Individual[ENVIRONMENT.PopulationSize];

            for (int i = 0; i < ENVIRONMENT.PopulationSize; i++)
            {
                ENVIRONMENT.cities = DataReader.ReadData();
                population[i] = new Individual();
                population[i].Order = new int[ENVIRONMENT.IndividualSize];
                population[i].Cities = new City[ENVIRONMENT.IndividualSize];
                int currentlyPossibleSize = ENVIRONMENT.IndividualSize;
                population[i].RemainingCities = ENVIRONMENT.cities;
                for (int j = 0; j < ENVIRONMENT.IndividualSize; j++)
                {
                    City randomCity = population[i].GetRandomCity(currentlyPossibleSize);
                    //wylosuj jedno miasto
                    population[i].Cities[j] = randomCity;

                    //zapisz jego Index w Individual.Order
                    population[i].Order[j] = randomCity.Index;
                    //dodaj odległość do całkowitego dystansu
                    population[i].TotalDistance += DistanceHelper.FindDistance(randomCity, j > 0 ? population[i].Cities[j - 1] : randomCity);
                    //usuń to miasto z możliwych do wylosowania
                    population[i].RemoveFromPossible(population[i].RemainingCities, randomCity);
                    //zmniejsz tablicę możliwości
                    currentlyPossibleSize--;
                }
                population[i].TotalDistance += DistanceHelper.FindDistance(population[i].Cities[0], population[i].Cities[ENVIRONMENT.IndividualSize - 1]);
                Console.WriteLine(population[i].ToString());

            }

            return population;

        }
    }
    public class Criteria
    {
        public enum SelectionModes
        {
            Contest = 1,
            Roulette = 2,
            RankedRoulette = 3
        }
        public enum StopCriterias
        {
            GenerationCount = 1,
            Time = 2
        }

        public SelectionModes? SelectionMode { get; set; }
        public StopCriterias? StopCriteria { get; set; }
        public int? ContestSize { get; set; }
        public int? PopulationSize { get; set; }
        public int? GenerationCount { get; set; }
        public TimeSpan? Time { get; set; }
        public DateTime FinishTime { get; set; }
        public double? MinRange { get; set; }
        public double? MaxRange { get; set; }

        public override string ToString()
        {
            return string.Format("Selection type: {0} \nStop criteria: {1}\nContest size: {2}\nPopulation size: {3}\n" +
                "Generation count: {4}\nTime: {5}\nMin range: {6}\nMax range: {7}", SelectionMode.ToString(), StopCriteria.ToString(),
                ContestSize, PopulationSize, GenerationCount, Time, MinRange, MaxRange);
        }
        class Program
        {
            static void Main(string[] args)
            {
                Criteria criteria = new Criteria()
                {
                    FinishTime = DateTime.Parse(args[0]),
                    ContestSize = int.Parse(args[1]),
                    SelectionMode = (Criteria.SelectionModes)(int.Parse(args[2])),
                    PopulationSize = int.Parse(args[3]),
                    GenerationCount = int.Parse(args[4])
                };
            }
        }
    }
}