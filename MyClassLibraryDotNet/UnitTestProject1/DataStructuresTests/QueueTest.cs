using DataStructures.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class QueueTest
    {
        [TestMethod]
        public static void DoTest()
        {
            string top;
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");
            queue.Enqueue("ddd");
            queue.Enqueue("eee");
            queue.Enqueue("fff");
            queue.Enqueue("ggg");
            queue.Enqueue("hhh");
            Assert.AreEqual(8, queue.Count);

            var array = queue.ToArray();
            // fails if wrong size
            Assert.AreEqual(8, array.Length);

            queue.Dequeue();
            queue.Dequeue();
            top = queue.Dequeue();
            Assert.AreEqual("ccc", top);

            queue.Dequeue();
            queue.Dequeue();
            Assert.AreEqual("fff", queue.Top);

            var array2 = queue.ToArray();
            // fails if wrong size
            Assert.AreEqual(3, array2.Length);
        }
    }
}

