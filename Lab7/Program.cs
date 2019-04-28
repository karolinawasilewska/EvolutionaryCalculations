using System;
using Lab8.Helpers;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
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

            //wybierz rodziców
            Individual mum = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];
            Individual dad = currentPopulation.Individuals[ENVIRONMENT.random.Next(currentPopulation.Individuals.Length - 1)];
            //skrzyżuj rodziców
            //sprawdź czy dziecko jest poprawne, jesli nie, wylosuj skrzyżuj jeszcze raz
            //mutacja
            //powtarzaj do wypełnienia populacji


            Console.ReadKey();
        }
    }
}
