using System;
using Lab8.Helpers;

namespace Lab8.Crossovers
{
    public class OX : ICrossover
    {
        public Individual Crossover(Individual parent1, Individual parent2)
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

            Individual child = new Individual();

            if (parent1.Cities.Length != parent2.Cities.Length)
                throw new Exception("Invalid parents");

            int cut1, cut2;

            cut1 = ENVIRONMENT.random.Next(1, parent1.Cities.Length - 1);
            cut2 = ENVIRONMENT.random.Next(cut1 + 1, parent1.Cities.Length);

            int[] o1temp = new int[parent1.Cities.Length];
            int[] o1 = new int[parent1.Cities.Length];

            int o1tempIndex = 0;
            for (int i = cut2+1; i < o1temp.Length+1; i++)
            {
                o1temp[o1tempIndex] = parent2.Cities[i-1].Index;
                o1tempIndex++;
            }
            for (int i = 0; i < cut2; i++)
            {
                o1temp[o1tempIndex] = parent2.Cities[i].Index;
                o1tempIndex++;
            }

            int[] o1temp2 = new int[o1temp.Length - (cut2 - cut1)];
            o1tempIndex = 0;
            int[] removed = new int[cut2 - cut1];
            for (int i = cut1; i < cut2; i++)
            {
                removed[o1tempIndex] = parent1.Cities[i].Index;
                o1tempIndex++;
            }
            o1tempIndex = 0;

            int[] remained = new int[parent1.Cities.Length - removed.Length];

            foreach (var item in o1temp)
            {
                if (!removed.ContainsInt(item))
                {
                    remained[o1tempIndex] = item;
                    o1tempIndex++;
                }
            }
            o1tempIndex = 0;
            for (int i = cut2; i < parent1.Cities.Length; i++)
            {
                o1[i] = remained[o1tempIndex];
                o1tempIndex++;
            }
            for (int i = 0; i < cut1; i++)
            {
                o1[i] = remained[o1tempIndex];
                o1tempIndex++;
            }
            o1tempIndex = 0;
            for (int i = cut1; i < cut2; i++)
            {
                o1[i] = removed[o1tempIndex];
                o1tempIndex++;
            }

            return child;
        }
    }
}
