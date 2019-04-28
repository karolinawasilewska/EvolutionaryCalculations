using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public static class ENVIRONMENT
    {
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
        public static City[] cities { get;
            set; }
        public static Random random { get; set; } = new Random();
    }
}
