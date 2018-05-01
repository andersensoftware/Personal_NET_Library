using DataStructures.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class StackTest
    {
        [TestMethod]
        public static void DoTest()
        {
            int top;
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            // Wrong top value.
            Assert.AreEqual(6, stack.Top);

            var array = stack.ToArray();

            // Wrong size!
            Assert.AreEqual(array.Length, stack.Count);

            top = stack.Pop();

            // Wrong top value.
            Assert.AreEqual(5, stack.Top);

            stack.Pop();
            stack.Pop();

            // Wrong top value.
            Assert.AreEqual(3, stack.Top);

            var array2 = stack.ToArray();

            // Wrong size!
            Assert.AreEqual(array2.Length, stack.Count);
        }
    }
}

