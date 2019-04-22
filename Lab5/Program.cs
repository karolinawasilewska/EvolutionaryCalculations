using Lab5.Helpers;
using Lab5.Objects;
using Lab5.Selections;
using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static FileReaderHelper fileWriterHelper = new FileReaderHelper();
        static void Main(string[] args)
        {
            List<Criteria> criteria = fileWriterHelper.Read();

            foreach (var criterium in criteria)
            {
                Console.WriteLine(criterium.ToString());
                Selection selection = new Selection();
                if (!string.IsNullOrEmpty(selection.ValidateCriteria(criterium)))
                {
                    Console.WriteLine(selection.ValidateCriteria(criterium));
                    break;
                }

                Range<double> range = new Range<double>(criterium.MinRange ?? 0.0, criterium.MaxRange ?? 0.0);

                switch (criterium.SelectionMode)
                {
                    case Criteria.SelectionModes.Contest:
                        selection = new Contest();
                        if (!string.IsNullOrEmpty(selection.ValidateCriteria(criterium)))
                        { Console.WriteLine(selection.ValidateCriteria(criterium)); break; }
                        else
                        {
                            Population population = new Population(criterium.PopulationSize ??0, range);
                            population.PopulationInit();
                            Contest.ContestSize = criterium.ContestSize??0;

                            switch (criterium.StopCriteria)
                            {
                                case Criteria.StopCriterias.GenerationCount:
                                    selection.DoWork(population, criterium);
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case Criteria.SelectionModes.RankedRoulette:
                        selection = new RankedRouletteSelection();
                        if (!string.IsNullOrEmpty(selection.ValidateCriteria(criterium)))
                        { Console.WriteLine(selection.ValidateCriteria(criterium)); break; }
                        else
                        {
                            //todo
                        }
                        break;
                    case Criteria.SelectionModes.Roulette:
                        selection = new RouletteSelection();
                        if (!string.IsNullOrEmpty(selection.ValidateCriteria(criterium)))
                        { Console.WriteLine(selection.ValidateCriteria(criterium)); break; }
                        else
                        {
                            //todo
                        }
                        break;
                    default:
                        break;
                }

                




                //for (int i = 0; i < 1000; i++)
                //{
                //    if (i % 50 == 0)
                //        Console.WriteLine(population.TheBestInPopulation().FunctionValue);
                //    population = Contest.NewPopulationInit(population);
                //}
            }
            Console.ReadKey();
        }
    }
}
