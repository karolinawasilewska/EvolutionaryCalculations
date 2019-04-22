using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Objects
{
    public class Criteria
    {
        public enum SelectionModes{
            Contest=1,
            Roulette = 2,
            RankedRoulette=3
        }
        public enum StopCriterias
        {
            GenerationCount=1,
            Time=2
        }

        public SelectionModes? SelectionMode { get; set; }
        public StopCriterias? StopCriteria { get; set; }
        public int? ContestSize { get; set; }
        public int? PopulationSize { get; set; }
        public int? GenerationCount { get; set; }
        public TimeSpan? Time { get; set; }
        public double? MinRange { get; set; }
        public double? MaxRange { get; set; }

        public override string ToString()
        {
            return string.Format("Selection type: {0} \nStop criteria: {1}\nContest size: {2}\nPopulation size: {3}\n" +
                "Generation count: {4}\nTime: {5}\nMin range: {6}\nMax range: {7}", SelectionMode.ToString(), StopCriteria.ToString(),
                ContestSize, PopulationSize, GenerationCount, Time, MinRange, MaxRange);
        }
    }
}
