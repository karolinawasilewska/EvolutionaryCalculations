using System;
using Lab8.Helpers;

namespace Lab8.Crossovers
{
    public class OX : ICrossover
    {
        public Individual[] Crossover(Individual parent1, Individual parent2)
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
            //    new City() { Index = 5 },
            //    new City() { Index = 2 },
            //    new City() { Index = 1 },
            //    new City() { Index = 8 },
            //    new City() { Index = 7 },
            //    new City() { Index = 6 },
            //    new City() { Index = 9 },
            //    new City() { Index = 3 }
            //};

            parent1.CreateOrder();
            parent2.CreateOrder();

            Individual child1 = new Individual();
            Individual child2 = new Individual();

            if (parent1.Cities.Length != parent2.Cities.Length)
                throw new Exception("Invalid parents");

            int cut1, cut2;

            cut1 = ENVIRONMENT.random.Next(1, parent1.Order.Length - 1);
            cut2 = ENVIRONMENT.random.Next(cut1 + 1, parent1.Order.Length);

            int[] o1temp = new int[parent1.Order.Length];
            // int[] o1 = new int[parent1.Order.Length];
            int[] o2temp = new int[parent1.Order.Length];
            //int[] o2 = new int[parent1.Order.Length];

            int o1tempIndex = 0;
            for (int i = cut2 + 1; i < o1temp.Length + 1; i++)
            {
                o1temp[o1tempIndex] = parent2.Order[i - 1];
                o2temp[o1tempIndex] = parent1.Order[i - 1];
                o1tempIndex++;
            }
            for (int i = 0; i < cut2; i++)
            {
                o1temp[o1tempIndex] = parent2.Order[i];
                o2temp[o1tempIndex] = parent1.Order[i];
                o1tempIndex++;
            }

           // int[] o1temp2 = new int[o1temp.Length - (cut2 - cut1)];
            o1tempIndex = 0;
            int[] removed1 = new int[cut2 - cut1];
            int[] removed2 = new int[cut2 - cut1];
            for (int i = cut1; i < cut2; i++)
            {
                removed1[o1tempIndex] = parent1.Order[i];
                removed2[o1tempIndex] = parent2.Order[i];
                o1tempIndex++;
            }
            o1tempIndex = 0;

            int[] remained1 = new int[parent1.Order.Length - removed1.Length];
            int[] remained2 = new int[parent1.Order.Length - removed2.Length];

            foreach (var item in o1temp)
            {
                if (!removed1.ContainsInt(item))
                {
                    remained1[o1tempIndex] = item;
                    o1tempIndex++;
                }
            }
            o1tempIndex = 0;
            foreach (var item in o2temp)
            {
                if (!removed2.ContainsInt(item))
                {
                    remained2[o1tempIndex] = item;
                    o1tempIndex++;
                }
            }
            o1tempIndex = 0;
            for (int i = cut2; i < parent1.Order.Length; i++)
            {
                child1.Order[i] = remained1[o1tempIndex];
                child2.Order[i] = remained2[o1tempIndex];
                o1tempIndex++;
            }
            for (int i = 0; i < cut1; i++)
            {
                child1.Order[i] = remained1[o1tempIndex];
                child2.Order[i] = remained2[o1tempIndex];
                o1tempIndex++;
            }
            o1tempIndex = 0;
            for (int i = cut1; i < cut2; i++)
            {
                child1.Order[i] = removed1[o1tempIndex];
                child2.Order[i] = removed2[o1tempIndex];
                o1tempIndex++;
            }

            //child.Order = o1;

            return new Individual[] { child1, child2 };
        }
    }
}
