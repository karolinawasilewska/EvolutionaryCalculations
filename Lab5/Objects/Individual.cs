using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Objects
{
    public class Individual
    {
        private double accuracy;
        private float mutationProb;
        private int mutatedGenes;
      //  private Range<int> range;
        public Individual(double accuracy, float mutationProb, int mutatedGenes, Range<int> range)
        {
            this.accuracy = accuracy;
            this.mutationProb = mutationProb;
            this.mutatedGenes = mutatedGenes;
          //  this.range = range;
        }
        public Individual() {
        //    range = new Range<int>(-2, 2);
            accuracy = 0.000000001;
        }
       static Random rand = new Random();

        public uint Genotype { get; set; }

        public double Phenotype
        {
            get
            {
                //if (range.Min * range.Max < 0 || range.Max < 0)//są po dwóch stronach 0 na osi
                //{
                //    return range.Min + Genotype * accuracy;
                //}
                //else
                //{
                //    return Genotype * accuracy;
                //}
              return  -2.0 + Genotype * accuracy;
            }
        }

        public double FunctionValue
        {
            get
            {
                return Environment.Function(Phenotype);
            }
        }

        public bool OutOfRange(Range<double> range)
        {
            return Phenotype > range.Max || Phenotype < range.Min;
        }

        public Individual Mutate(uint genotype)
        {
            uint mask = (uint)Math.Pow(2, rand.Next(32));
            return new Individual() { Genotype = mask ^ genotype };
        }

        public bool MutationNeeded()
        {
            return rand.NextDouble() < mutationProb;
        }

        public Individual Crossover(Individual mum, Individual dad)
        {
            int splitPoint = rand.Next(32);
            uint mask = uint.MaxValue << splitPoint;

            return new Individual()
            {
                Genotype = (dad.Genotype & mask) | (mum.Genotype & ~mask)
            };
        }
    }
}
