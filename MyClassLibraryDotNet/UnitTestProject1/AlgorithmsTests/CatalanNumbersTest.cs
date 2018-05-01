using Algorithms.Numeric;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace UnitTest.AlgorithmsTests
{
    [TestClass]
    public class CatalanNumbersTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var list = CatalanNumbers.GetRange(0, 100);
            var list2 = new List<ulong>();

            // TRY CALCULATING FROM Bin.Coeff.
            for (uint i = 0; i < list.Count; ++i)
            {
                var catalanNumber = CatalanNumbers.GetNumberByBinomialCoefficients(i);
                list2.Add(catalanNumber);

                Assert.IsTrue(list[(int)i] == list2[(int)i], "Wrong calculation.");
            }
        }
    }
}
