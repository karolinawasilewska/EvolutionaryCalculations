using Lab4.Objects;
using Lab4.Selections;
using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Population population = new Population();
            population.PopulationInit();

            for (int i = 0; i < 1000; i++)
            {
                if (i % 50 == 0)
                    Console.WriteLine(population.TheBestInPopulation().FunctionValue);
                population= Contest.NewPopulationInit(population);
            }
            Console.ReadKey();
        }
    }
}
