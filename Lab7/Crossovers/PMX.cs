using Lab8.Helpers;
using System;

namespace Lab8.Crossovers
{
    public class PMX 
    {
        public static Individual[] Crossover(Individual parent1, Individual parent2)
        {
            parent1.Cities = new City[] {
                new City() { Index = 1 },
                new City() { Index = 2 },
                new City() { Index = 3 },
                new City() { Index = 4 },
                new City() { Index = 5 },
                new City() { Index = 6 },
                new City() { Index = 7 },
                new City() { Index = 8 },
                new City() { Index = 9 }
            };

            parent2.Cities = new City[] {
                new City() { Index = 4 },
                new City() { Index = 5 },
                new City() { Index = 2 },
                new City() { Index = 1 },
                new City() { Index = 8 },
                new City() { Index = 7 },
                new City() { Index = 6 },
                new City() { Index = 9 },
                new City() { Index = 3 }
            };
            parent1.CreateOrder();
            parent2.CreateOrder();

            Individual child1 = new Individual() { Cities = new City[parent1.Order.Length] };
            Individual child2 = new Individual() { Cities = new City[parent1.Order.Length] }; 

            if (parent1.Cities.Length != parent2.Cities.Length)
                throw new Exception("Invalid parents");

            int cut1, cut2;

            cut1 = ENVIRONMENT.random.Next(1, parent1.Order.Length - 1);
            cut2 = ENVIRONMENT.random.Next(cut1 + 1, parent1.Order.Length);

            // int[] tempo1 = new int[cut2 - cut1];
            // int[] tempo2 = new int[cut2 - cut1];

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i >= cut1 && i < cut2)
                {
                    child1.Order[i] = parent2.Order[i];
                    child2.Order[i] = parent1.Order[i];
                }
            }

            for (int i = 0; i < child1.Order.Length; i++)
            {
                if (i < cut1 || i >= cut2)
                {
                    if (!child1.Order.ContainsInt(parent1.Order[i]))
                    {
                        child1.Order[i] = parent1.Order[i];
                    }
                    else
                    {
                        child1.Order[i] = child2.Order[Array.IndexOf(child1.Order, parent1.Order[i])];
                    }
                    if (!child2.Order.ContainsInt(parent2.Order[i]))
                    {
                        child2.Order[i] = parent2.Order[i];
                    }
                    else
                    {
                        child2.Order[i] = child1.Order[Array.IndexOf(child2.Order, parent2.Order[i])];
                    }
                }
            }

            return new Individual[] { child1, child2 };
        }
    }
}
