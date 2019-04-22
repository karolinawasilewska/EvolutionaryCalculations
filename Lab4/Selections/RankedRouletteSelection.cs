using Lab4.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Selections
{
    public class RankedRouletteSelection
    {
        static Random rand = new Random();
        public static Individual RankedRouletteSelect(Population population)
        {
            var sorted = population.Individuals.OrderBy(x => x.FunctionValue).ToArray();
            var sumArray = ((population.Individuals.Count - 1) / 2) * population.Individuals.Count;
            Individual parent = null;
            var i = 0;
            var sum = 0;
            var rankedChosenValue =rand.Next(1, sumArray);
            while (parent == null)
            {
                if (rankedChosenValue <= sum)
                {
                    parent = sorted[i];
                }
                i++;
                sum += i;
            }
            return parent;
        }
        public static Population RankedRoulettePopulationInit(Population old)
        {
            Population population = new Population();
            population.Individuals = new List<Individual>();

            for (int i = 0; i < old.Individuals.Count; i++)
            {
                do
                {
                    Individual dad = RankedRouletteSelect(old);
                    Individual mum = RankedRouletteSelect(old);

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
