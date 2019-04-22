using Lab4.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Selections
{
    public static class Contest
    {
        public static Individual ContestSelection(Population poplation, int contestSize)
        {
            Random rand = new Random();
            List<Individual> individuals = new List<Individual>();
            for (int i = 0; i < contestSize; i++)
            {
                individuals.Add(poplation.Individuals[rand.Next(poplation.Individuals.Count)]);
            }
            return individuals.OrderByDescending(q => q.FunctionValue).FirstOrDefault();
        }
        public static Population NewPopulationInit(Population old)
        {
            Population population = new Population();
            population.Individuals = new List<Individual>();

            for (int i = 0; i < old.Individuals.Count; i++)
            {
                do
                {
                    Individual dad = ContestSelection(old, 2);
                    Individual mum = ContestSelection(old, 2);

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
