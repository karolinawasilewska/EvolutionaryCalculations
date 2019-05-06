using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8.Selecions
{
    public class Roulette 
    {
        public Individual Select(Individual[] individuals, double sum)
        {
            Individual un =individuals[2];
            double check = 0;
            double s = ENVIRONMENT.random.NextDouble() * sum;
            for (int i = 2; i < individuals.Length; i++)
            {
                check += individuals[i].TotalDistance;
                if (check > s)
                    break;

                un = individuals[i];
               
            }
            return un;
        }

    }
}
