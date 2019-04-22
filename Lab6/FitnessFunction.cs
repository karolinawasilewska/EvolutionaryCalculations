using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    class FitnessFunction
    {
        //pitagoras
        public double CalculateDistance(City city1, City city2)
        {
            return (Math.Pow(city1.Latitude - city1.Longitude, 2) + Math.Pow(city2.Latitude - city2.Longitude, 2));
        }
    }
}
