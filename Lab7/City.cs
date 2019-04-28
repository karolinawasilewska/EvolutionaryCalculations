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

        public override string ToString()
        {
            return string.Format("Index: {0}, X: {1}, Y: {2}", Index, Longitude, Latitude);
        }


    }
}
