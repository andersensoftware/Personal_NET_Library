using System.Collections.Generic;
using Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.AlgorithmsTests
{
    [TestClass]
    public static class HeapSorterTest
    {
        [TestMethod]
        public static void DoTest()
        {
            int[] numbersList1 = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            List<long> numbersList2 = new List<long> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };

            numbersList1.HeapSort();

            // Sort Ascending (same as the method above);
            numbersList2.HeapSortAscending();

            Assert.IsTrue(numbersList2[numbersList2.Count - 1] == numbersList2[numbersList2.Count - 1]);

            // Sort Descending
            numbersList2.HeapSortDescending();

            Assert.IsTrue(numbersList2[0] > numbersList2[numbersList2.Count - 1]);
        }
    }
}
