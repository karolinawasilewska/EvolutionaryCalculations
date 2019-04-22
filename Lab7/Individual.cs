using Lab8.Helpers;
using System;

namespace Lab8
{
    public class Individual
    {
        public City[] Cities { get; set; }
        public int[] Order { get; set; }
        public City[] RemainingCities { get; set; }

        public decimal TotalDistance { get; set; } = 0;

        public City GetRandomCity(int currentlyPossibleSize)
        {
            int index = ENVIRONMENT.random.Next(currentlyPossibleSize);
            return RemainingCities[index];
        }
        public void RemoveFromPossible(City[] remainingCities, City randomCity)
        {
            int randomCityIndex = remainingCities.IndexOf(randomCity);
            RemainingCities = remainingCities;
            for (int i = randomCityIndex; i < remainingCities.Length-1; i++)
            {
                if (i == remainingCities.Length-1)
                    RemainingCities[i] = null;
                else
                    RemainingCities[i] = remainingCities[i + 1];
            }

            Array.Resize(ref remainingCities, RemainingCities.Length - 1);
            RemainingCities = remainingCities;
        }

        public override string ToString()
        {
            string text = string.Format("Total disctance: {0}; Miasta: ", TotalDistance);
            foreach (var item in Cities)
            {
                text = string.Format("{0}, {1}", text, item.Index);
            }
            return text;
        }

    }
}
