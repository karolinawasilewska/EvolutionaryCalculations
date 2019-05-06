using System;
using Lab8.Helpers;
using Lab8.Crossovers;
using Lab8.Selecions;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            //Criteria criteria = new Criteria()
            //{
            //    FinishTime = DateTime.Parse(args[0]),
            //    ContestSize = int.Parse(args[1]),
            //    SelectionMode = (Criteria.SelectionModes)(int.Parse(args[2])),
            //    PopulationSize = int.Parse(args[3]),
            //    GenerationCount = int.Parse(args[4])
            //};
            //CX pmxCrossover = new CX();
            ////PMX pmxCrossover = new PMX();
            //Individual[] childpmx = pmxCrossover.Crossover(new Individual(), new Individual());

            ENVIRONMENT.cities = DataReader.ReadData();
            if (ENVIRONMENT.cities.Length != ENVIRONMENT.IndividualSize)
            {
                Console.WriteLine("Liczba miast nie jest zgodna z ilością danych");
                Console.ReadKey();
                throw new Exception();
            }
            Population currentPopulation = new Population()
            {
                Individuals = Population.GetRandomPopulation()
            };

            //  Population currentPopulation = new Population();
            for (int i = 0; i < ENVIRONMENT.PopulationCount; i++)
            {
                Population newPopulation = new Population()
                {
                    Individuals = new Individual[ENVIRONMENT.IndividualSize]
                };

                for (int individualIndex = 0; individualIndex < ENVIRONMENT.PopulationSize; individualIndex += 2)
                {

                    OX oxCrossover = new OX();
                    Contest contest = new Contest();
                    Roulette roulette = new Roulette();

                    //wybierz rodziców

                    double sum = 0.0;
                    for (int j = 0; j < currentPopulation.Individuals.Length; j++)
                    {
                        sum += currentPopulation.Individuals[j].TotalDistance;
                        //sum += (100000.0 / currentPopulation.Individuals[i].TotalDistance);
                    }

                    Individual mum = roulette.Select(currentPopulation.Individuals, sum);
                    Individual dad = roulette.Select(currentPopulation.Individuals, sum);

                    //  Individual mum = contest.Select(currentPopulation.Individuals, 2);
                    //  Individual dad = contest.Select(currentPopulation.Individuals, 2);

                    //Individual mum = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];
                    //Individual dad = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];

                    //skrzyżuj rodziców
                    Individual[] child = oxCrossover.Crossover(mum, dad);

                    newPopulation.Individuals[individualIndex] = child[0];
                    newPopulation.Individuals[individualIndex + 1] = child[1];

                    //sprawdź czy dziecko jest poprawne, jesli nie, wylosuj skrzyżuj jeszcze raz
                    //mutacja
                    //powtarzaj do wypełnienia populacji

                    currentPopulation = newPopulation;
                }
            }

            Console.ReadKey();
        }
    }
}
