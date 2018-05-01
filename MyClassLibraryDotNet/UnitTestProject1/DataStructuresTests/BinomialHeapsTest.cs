using DataStructures.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace C_Sharp_Algorithms.DataStructuresTests
{
    [TestClass]
    public static class BinomialHeapsTest
    {
        [TestMethod]
        public static void DoTest()
        {
            int i = 37;
            int numberOfItems = 100000;
            BinomialMinHeap<int> firstHeap = new BinomialMinHeap<int>();
            BinomialMinHeap<int> secondHeap = new BinomialMinHeap<int>();
            BinomialMinHeap<int> thirdHeap = new BinomialMinHeap<int>();

            for (i = 37; i != 0; i = (i + 37) % numberOfItems)
            {
                if (i % 2 == 0)
                    secondHeap.Add(i);
                else
                    firstHeap.Add(i);
            }

            firstHeap.Merge(secondHeap);
            thirdHeap = firstHeap;

            for (i = 1; i <= thirdHeap.Count; i++)
            {
                var min = thirdHeap.ExtractMin();
                Assert.IsTrue(min == i, "WRONG MIN");
            }

            Assert.IsTrue(secondHeap.IsEmpty, "SECOND HEAP SHOULD BE EMPTY");
        }
    }
}
