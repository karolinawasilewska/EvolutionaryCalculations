using System;

namespace Lab8.Crossovers
{
    interface ICrossover
    {
        Individual Crossover(Individual parent1, Individual parent2);
    }
}
