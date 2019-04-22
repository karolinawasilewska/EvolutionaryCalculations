using Lab5.Objects;
using System;
using System.Collections.Generic;
//using System.Linq;

namespace Lab5.Selections
{
    public class Contest : Selection
    {
        public static int ContestSize { get; set; }
      
        public override Population GenerateNewPopulation(Population oldPopulation)
        {

            Population population = base.GenerateNewPopulation(oldPopulation);

            population.Individuals = new List<Individual>();

            for (int i = 0; i < oldPopulation.Individuals.Count; i++)
            {
                do
                {
                    Individual[] parents = GetParents(oldPopulation);
                   

                    Individual child = new Individual();
                    child = child.Crossover(parents[0], parents[1]);

                    if (child.MutationNeeded())
                        child.Mutate(child.Genotype);

                    population.Individuals.Add(child);

                } while (population.Individuals[i].OutOfRange(population.Range));

            }
            return population;
        }

        public override Individual[] GetParents(Population population)
        {
            Random rand = new Random();
            Individual[] individuals = base.GetParents(population);
            for (int i = 0; i < 2; i++)
            {
                List<Individual> contest = new List<Individual>();
                for (int j = 0; j < ContestSize; j++)
                {
                    contest.Add(population.Individuals[rand.Next(population.Individuals.Count)]);
                }

                individuals[i] = GetBetterOne(contest);
            }

            return individuals;
        }

        private Individual GetBetterOne(List<Individual> individuals)
        {
            Individual theBest = individuals[0];
            foreach (var item in individuals)
            {
                if (item.FunctionValue > theBest.FunctionValue)
                    theBest = item;
            }
            return theBest;
        }

        public override Population DoWork(Population population, Criteria criteria)
        {
            switch (criteria.StopCriteria)
            {
                case Criteria.StopCriterias.GenerationCount:
                    for (int i = 0; i < criteria.GenerationCount; i++)
                    {
                        population = GenerateNewPopulation(population);
                    }
                    break;
                case Criteria.StopCriterias.Time:
                    break;
                default:
                    break;
            }
            return population;
        }

        public override string ValidateCriteria(Criteria criteria)
        {
            string err = base.ValidateCriteria(criteria);

            //todo

            return err;
        }
    }

}
