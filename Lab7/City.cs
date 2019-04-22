using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
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


      
    }
}
