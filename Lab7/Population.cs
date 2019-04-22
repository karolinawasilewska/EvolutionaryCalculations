using Lab8.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public class Population
    {
        public Individual[] Individuals{ get; set; }
        

        public static Individual[] GetRandomPopulation()
        {
            Individual[] population = new Individual[ENVIRONMENT.PopulationSize];

            for (int i = 0; i < ENVIRONMENT.PopulationSize; i++)
            {
                population[i] = new Individual();
                population[i].Order = new int[ENVIRONMENT.IndividualSize];
                population[i].Cities = new City[ENVIRONMENT.IndividualSize];
                int currentlyPossibleSize = ENVIRONMENT.IndividualSize;
                population[i].RemainingCities = ENVIRONMENT.cities;
                for (int j = 0; j < ENVIRONMENT.IndividualSize; j++)
                {
                    City randomCity = population[i].GetRandomCity(currentlyPossibleSize);
                    //wylosuj jedno miasto
                    population[i].Cities[j] = randomCity;

                    //zapisz jego Index w Individual.Order
                    population[i].Order[j] = randomCity.Index;
                    //dodaj odległość do całkowitego dystansu
                    population[i].TotalDistance += DistanceHelper.FindDistance(randomCity, j > 0 ? population[i].Cities[j - 1] : randomCity);
                    //usuń to miasto z możliwych do wylosowania
                    population[i].RemoveFromPossible(population[i].RemainingCities, randomCity);
                    //zmniejsz tablicę możliwości
                    currentlyPossibleSize--;
                }
                population[i].TotalDistance+= DistanceHelper.FindDistance(population[i].Cities[0], population[i].Cities[ENVIRONMENT.IndividualSize-1]);
                Console.WriteLine(population[i].ToString());

            }

            return population;

        }
    }
}
