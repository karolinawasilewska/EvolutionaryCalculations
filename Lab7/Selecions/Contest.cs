using System;
using Lab8.Helpers;

namespace Lab8.Selecions
{
    public class Contest
    {
        public Individual Select(Individual[] individuals, int size)
        {
            Individual[] contestans = new Individual[size];

            for (int i = 0; i < contestans.Length; i++)
            {
                contestans[i] = individuals[ENVIRONMENT.random.Next(individuals.Length - 1)];
            }

            Individual best = individuals[0];
            double minDistance = DistanceHelper.CountDistance(contestans[0].Cities);
            for (int i = 1; i < contestans.Length; i++)
            {
                double currentDistance = DistanceHelper.CountDistance(contestans[i].Cities);
                if (minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    best = contestans[i];
                }
            }
            return best;
        }
    }
}
