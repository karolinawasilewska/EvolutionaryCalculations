using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Objects
{
    public class Population
    {
        Random rand = new Random();
        public List<Individual> Individuals { get; set; }
        public const int PopulationCount = 20;

        public static Individual TheBestIndividual = null;

        public Individual TheBestInPopulation()
        {
            return Individuals.OrderByDescending(q => q.FunctionValue).FirstOrDefault();
        }

        public void PopulationInit()
        {
            Individuals = new List<Individual>();
            for (int i = 0; i < PopulationCount; i++)
            {
                do
                {
                    Individuals.Add(new Individual()
                    {
                        Genotype = (uint)i * (uint.MaxValue / PopulationCount) + (uint)rand.Next()
                    });
                } while (Individuals[i].OutOfRange());
            }
            TheBestIndividual = TheBestInPopulation();
        }
    }

}
