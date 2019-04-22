using Lab4.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4.Selections
{
    public class RouletteSelection
    {
        static Random rand = new Random();
        public static Individual RouletteSelecion(Population population)
        {
            var sum = population.Individuals.Sum(x => x.FunctionValue);
            Individual parent = null;
            var i = 0;
            var valuesSum = population.Individuals[i].FunctionValue;
            var rankedChosenValue = rand.Next(1, (int)sum);
            while (parent == null)
            {
                if (rankedChosenValue <= sum)
                {
                    parent = population.Individuals[i];
                }
                i++;
                valuesSum += i;
            }
            if (parent == null)
            {
                parent = population.Individuals[population.Individuals.Count - 1];
            }
            return parent;
        }

        public static Population RoulettePopulationInit(Population old)
        {
            Population population = new Population();
            population.Individuals = new List<Individual>();

            for (int i = 0; i < old.Individuals.Count; i++)
            {
                do
                {
                    Individual dad = RouletteSelecion(old);
                    Individual mum = RouletteSelecion(old);

                    Individual child = new Individual();
                    child = child.Crossover(mum, dad);

                    if (child.MutationNeeded())
                        child.Mutate(child.Genotype);

                    population.Individuals.Add(child);

                } while (population.Individuals[i].OutOfRange());

            }
            return population;
        }
    }
}
