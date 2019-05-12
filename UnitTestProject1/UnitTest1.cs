using System;
using KarolinaWasilewska;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestAlgorithmCX()
        //{
        //    Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //        IndexOfCities = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //    };

        //    Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 3, 0, 1, 7, 6, 5, 8, 2, 4 },
        //        IndexOfCities = new int[] { 1, 2, 7, 0, 8, 5, 4, 3, 6 },
        //    };

        //    Individual child = Algorithms.crossingByCX(mother, father);

        //    Individual correctResult = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 0, 1, 2, 3, 6, 5, 8, 7, 4 }
        //    };

        //    Assert.AreEqual(correctResult, child);
        //}
        //[TestMethod]
        //public void TestAlgorithmCX2()
        //{
        //    Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //        IndexOfCities = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //    };

        //    Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 3, 0, 1, 7, 6, 5, 8, 2, 4 },
        //        IndexOfCities = new int[] { 1, 2, 7, 0, 8, 5, 4, 3, 6 },
        //    };

        //    Individual child = Algorithms.crossingByCX(mother, father);

        //    Individual correctResult = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 3, 0, 1, 7, 4, 5, 6, 2, 8 }
        //    };

        //    Assert.AreEqual(correctResult, child);

        //}

        [TestMethod]
        public void TestAlgorithmPMX()
        {
            Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
            {
                Order = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            };

            Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
            {
                Order = new int[] { 4,5,2,1,8,7,6,9,3}
            };
            PMX pMX = new PMX(new KarolinaWasilewska.srodowisko.ProblemKlienta());
            Individual[] child = pMX.Crossover(mother.Order, father.Order);

            Individual[] correctResult = new Individual[]
            {
                new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
                {
                    Order = new int[] { 1,8,2,4,5,6,7,9,3}
                },
                new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
                {
                    Order = new int[] { 4,2,3,1,8,7,6,5,9}
                },

               
            };

            Assert.AreEqual(correctResult, child);
        }
        //[TestMethod]
        //public void TestAlgorithmPMX2()
        //{
        //    Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 3, 4, 1, 0, 7, 6, 5, 8, 2 },
        //        IndexOfCities = new int[] { 3, 2, 8, 0, 1, 6, 5, 4, 7 },
        //    };

        //    Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //        IndexOfCities = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
        //    };

        //    Individual child = Algorithms.crossingByPMX(mother, father, 3, 4);

        //    Individual correctResult = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 0, 7, 1, 3, 4, 5, 6, 8, 2 }
        //    };

        //    Assert.AreEqual(correctResult, child);
        //}

        //[TestMethod]
        //public void TestAlgorithmOX()
        //{
        //    Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        //    };

        //    Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 4, 5, 2, 1, 8, 7, 6, 9, 3 }
        //    };

        //    Individual child = Algorithms.crossingByOX(mother, father, 3, 4);

        //    Individual correctResult = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 2, 1, 8, 4, 5, 6, 7, 9, 3 }
        //    };

        //    Assert.AreEqual(correctResult, child);
        //}
        //[TestMethod]
        //public void TestAlgorithmOX2()
        //{
        //    Individual mother = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 4, 5, 2, 1, 8, 7, 6, 9, 3 },
        //    };

        //    Individual father = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Sequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
        //    };

        //    Individual child = Algorithms.crossingByOX(mother, father, 3, 4);

        //    Individual correctResult = new Individual(new KarolinaWasilewska.srodowisko.ProblemKlienta())
        //    {
        //        Individual = new int[] { 3, 4, 5, 1, 8, 7, 6, 9, 2 }
        //    };

        //    Assert.AreEqual(correctResult, child);
        //}
    }
}
