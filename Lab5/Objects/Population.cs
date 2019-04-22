using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5.Objects
{
    public class Population
    {
        Random rand = new Random();
        public List<Individual> Individuals { get; set; }
        public int PopulationSize { get; set; }
        public Range<double> Range { get; set; }

        public static Individual TheBestIndividual = null;

        public Individual TheBestInPopulation()
        {
            return Individuals.OrderByDescending(q => q.FunctionValue).FirstOrDefault();
        }

        public Population(int size, Range<double> range)
        {
            PopulationSize = size;
            Range = range;
        }
        public Population() { }
        public void PopulationInit()
        {
            Individuals = new List<Individual>();
            int i = 0;
            do
            {
                Individual individual = new Individual()
                {
                    Genotype = (uint)((uint)i * (uint.MaxValue / PopulationSize) + (uint)rand.Next())
                };

                if (!individual.OutOfRange(Range))
                {
                    Individuals.Add(individual);
                }
                i++;
            } while (Individuals.Count<=PopulationSize);
          
            TheBestIndividual = TheBestInPopulation();
        }
    }

}
