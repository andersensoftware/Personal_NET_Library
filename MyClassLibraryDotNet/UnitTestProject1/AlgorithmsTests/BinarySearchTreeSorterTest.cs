using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class BinarySearchTreeSorterTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var expectedSort = new List<int> { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.UnbalancedBSTSort<int>();

            Assert.IsTrue(numbers.SequenceEqual<int>(expectedSort));
        }
    }
}
