using Lab5.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Selections
{
    public class Selection
    {
        public virtual string ValidateCriteria(Criteria criteria)
        {
            string err = "";
            if (criteria.SelectionMode == null)
                err += "Selection mode can not be null\n";
            if (criteria.MinRange == null)
                err += "Min range can not be null\n";
            if (criteria.MaxRange == null)
                err += "Max range can not be null\n";
            return err;
        }
        public virtual Individual[] GetParents(Population population)
        {
            return new Individual[2];
        }
        public virtual Population GenerateNewPopulation(Population population)
        {
            return new Population(population.PopulationSize, population.Range);
        }
        public virtual Population DoWork(Population population, Criteria criteria)
        {
            return new Population();
        }
    }
}
