using Lab8.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8.Crossovers
{
    public class CX 
    {
        public static Individual[] Crossover(Individual parent1, Individual parent2)
        {
            //parent1.Cities = new City[] {
            //    new City() { Index = 1 },
            //    new City() { Index = 2 },
            //    new City() { Index = 3 },
            //    new City() { Index = 4 },
            //    new City() { Index = 5 },
            //    new City() { Index = 6 },
            //    new City() { Index = 7 },
            //    new City() { Index = 8 },
            //    new City() { Index = 9 }
            //};

            //parent2.Cities = new City[] {
            //    new City() { Index = 4 },
            //    new City() { Index = 1 },
            //    new City() { Index = 2 },
            //    new City() { Index = 8 },
            //    new City() { Index = 7 },
            //    new City() { Index = 6 },
            //    new City() { Index = 9 },
            //    new City() { Index = 3 },
            //    new City() { Index = 5 }
            //};

            parent1.CreateOrder();
            parent2.CreateOrder();

            Individual child1 = new Individual();
            Individual child2 = new Individual();
            child1.Order[0] = parent1.Order[0];
            child2.Order[0] = parent2.Order[0];

            int currentIndex = 0;
            while (!child1.Order.ContainsInt(parent2.Order[currentIndex]))
            {
                int test = parent2.Order[currentIndex];
                int index = Array.IndexOf(parent1.Order, test);
                child1.Order[index] = test;
                currentIndex = index;
            }
            currentIndex = 0;
            while (!child2.Order.ContainsInt(parent1.Order[currentIndex]))
            {
                int test = parent1.Order[currentIndex];
                int index = Array.IndexOf(parent2.Order, test);
                child2.Order[index] = test;
                currentIndex = index;
            }

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (child1.Order[i]==0)
                    child1.Order[i] = parent2.Order[i];
                if (child2.Order[i] == 0)
                    child2.Order[i] = parent1.Order[i];
            }


            return new Individual[] { child1, child2 };
        }
    }
}
