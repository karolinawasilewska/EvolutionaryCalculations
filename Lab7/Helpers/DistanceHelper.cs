using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8.Helpers
{
    public class DistanceHelper
    {
        public static double FindDistance(City city1, City city2)
        {
            double distance=
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

            distance += FindDistance(cities[cities.Length-1], cities[0]);

            return distance;

        }

    }
}
