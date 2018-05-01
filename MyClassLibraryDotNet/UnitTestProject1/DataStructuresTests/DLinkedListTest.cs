using DataStructures.Lists;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class DLinkedListTest
    {
        [TestMethod]
        public static void DoTest()
        {
            DLinkedList<string> listOfStrings = new DLinkedList<string>();

            listOfStrings.Append("zero");
            listOfStrings.Append("fst");
            listOfStrings.Append("sec");
            listOfStrings.Append("trd");
            listOfStrings.Append("for");
            listOfStrings.Append("fft");
            listOfStrings.Append("sxt");
            listOfStrings.Append("svn");
            listOfStrings.Append("egt");

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Assert.IsTrue(listOfStrings[0] == "fst", "Wrong first element.");

            // Remove 4th
            listOfStrings.RemoveAt(4);
            Console.WriteLine("Remove At 4:\r\n" + listOfStrings.ToReadable());
            Assert.IsTrue(listOfStrings[4] == "sxt", "Wrong 4th element.");

            // Remove 5th and 6th
            // Note that after removing 5th, the old element at index 6 becomes at index 5.
            listOfStrings.RemoveAt(5);
            listOfStrings.RemoveAt(5);
            Assert.IsTrue(listOfStrings[4] == "sxt", "Wrong element at index 5.");
            Assert.IsTrue(listOfStrings.Count < 6, "Wrong element at index 6. There must be no element at index 5.");

            // Remove 3rd
            listOfStrings.RemoveAt(listOfStrings.Count - 1);
            Assert.IsTrue(listOfStrings[3] == "for", "Wrong element at index 3.");

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Assert.IsTrue(listOfStrings[0] == "sec", "Wrong element at index 0.");

            listOfStrings.Prepend("semsem3");
            listOfStrings.Prepend("semsem2");
            listOfStrings.Prepend("semsem1");

            listOfStrings.InsertAt("InsertedAtLast1", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtLast2", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtMiddle", (listOfStrings.Count / 2));
            listOfStrings.InsertAt("InsertedAt 4", 4);
            listOfStrings.InsertAt("InsertedAt 9", 9);
            listOfStrings.InsertAfter("InsertedAfter 11", 11);

            // Test the remove item method
            listOfStrings.Remove("trd");

            listOfStrings.Remove("InsertedAt 9");
            var arrayVersion = listOfStrings.ToArray();
            Assert.IsTrue(arrayVersion.Length == listOfStrings.Count);

            /****************************************************************************************/

            var stringsIterators = listOfStrings.GetEnumerator();
            stringsIterators.MoveNext();
            Assert.IsTrue(stringsIterators.Current == listOfStrings[0], "Wrong enumeration.");
            if (stringsIterators.MoveNext() == true)
            {
                Assert.IsTrue(stringsIterators.Current == listOfStrings[1], "Wrong enumeration.");
            }

            stringsIterators.Dispose();
            Assert.IsTrue(listOfStrings != null && listOfStrings.Count > 0, "Enumartor has side effects!");

            /****************************************************************************************/
            var listOfNumbers = new DLinkedList<int>();
            listOfNumbers.Append(23);
            listOfNumbers.Append(42);
            listOfNumbers.Append(4);
            listOfNumbers.Append(16);
            listOfNumbers.Append(8);
            listOfNumbers.Append(15);
            listOfNumbers.Append(9);
            listOfNumbers.Append(55);
            listOfNumbers.Append(0);
            listOfNumbers.Append(34);
            listOfNumbers.Append(12);
            listOfNumbers.Append(2);

            listOfNumbers.SelectionSort();
            var intArray = listOfNumbers.ToArray();
            Assert.IsTrue(intArray[0] == 0 && intArray[intArray.Length - 1] == 55, "Wrong sorting!");
        }
    }
}

