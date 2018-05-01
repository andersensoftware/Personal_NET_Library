using Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.AlgorithmsTests
{
    [TestClass]
    public static class QuickSortTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var list = new List<long>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            list.QuickSort();
            long[] sortedList = { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            Assert.IsTrue(list.SequenceEqual(sortedList));
        }
    }
}

