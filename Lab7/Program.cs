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
            CX pmxCrossover = new CX();
            //PMX pmxCrossover = new PMX();
            Individual[] childpmx = pmxCrossover.Crossover(new Individual(), new Individual());

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

            OX oxCrossover = new OX();
            Contest contest = new Contest();

            //wybierz rodziców

            Individual mum = contest.Select(currentPopulation.Individuals, 2);
            Individual dad = contest.Select(currentPopulation.Individuals, 2);

            //Individual mum = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];
            //Individual dad = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];

            //skrzyżuj rodziców
             Individual[] child = oxCrossover.Crossover(mum, dad);

            //sprawdź czy dziecko jest poprawne, jesli nie, wylosuj skrzyżuj jeszcze raz
            //mutacja
            //powtarzaj do wypełnienia populacji


            Console.ReadKey();
        }
    }
}
