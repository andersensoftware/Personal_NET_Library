using DataStructures.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class SkipListTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var skipList = new SkipList<int>();

            for (int i = 100; i >= 50; --i)
                skipList.Add(i);

            for (int i = 0; i <= 35; ++i)
                skipList.Add(i);

            for (int i = -15; i <= 0; ++i)
                skipList.Add(i);

            for (int i = -15; i >= -35; --i)
                skipList.Add(i);

            Assert.IsTrue(skipList.Count == 124);

            skipList.Clear();

            for (int i = 100; i >= 0; --i)
                skipList.Add(i);

            Assert.IsTrue(skipList.Count == 101);
        }
    }
}
