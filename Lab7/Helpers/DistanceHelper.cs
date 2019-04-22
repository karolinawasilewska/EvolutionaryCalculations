using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8.Helpers
{
    public class DistanceHelper
    {
        public static decimal FindDistance(City city1, City city2)
        {
            double distance=
                Math.Sqrt(
                    Math.Pow(city1.Latitude - city2.Latitude, 2) +
                    Math.Pow(city1.Longitude - city2.Longitude, 2)); ;
            return decimal.Parse(distance.ToString());
        }
    }
}
