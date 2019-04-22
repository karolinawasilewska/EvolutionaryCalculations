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
            Population randomPopulation =new Population()
            {
                Individuals = Population.GetRandomPopulation()
            };



            Console.ReadKey();
        }
    }
}
