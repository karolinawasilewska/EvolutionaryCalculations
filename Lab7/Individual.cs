using Lab8.Helpers;
using System;

namespace Lab8
{
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
           TotalDistance= DistanceHelper.CountDistance(Cities);
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
}
