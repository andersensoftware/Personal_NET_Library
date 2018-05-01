﻿using Algorithms.Sorting;
using DataStructures.Heaps;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class MinHeapTest
    {
        [TestMethod]
        public static void DoTest()
        {
            BinaryMinHeap<long> minHeap = new BinaryMinHeap<long>(Comparer<long>.Default);

            minHeap.Add(23);
            minHeap.Add(42);
            minHeap.Add(4);
            minHeap.Add(16);
            minHeap.Add(8);
            minHeap.Add(15);
            minHeap.Add(9);
            minHeap.Add(55);
            minHeap.Add(0);
            minHeap.Add(34);
            minHeap.Add(12);
            minHeap.Add(2);
            minHeap.Add(93);
            minHeap.Add(14);
            minHeap.Add(27);

            var array = minHeap.ToArray();
            Assert.IsTrue(array.Length == minHeap.Count, "Wrong size.");

            var list = minHeap.ToList();
            Assert.IsTrue(list.Count == minHeap.Count, "Wrong size.");

            array.HeapSortDescending();
            var maxHeap = minHeap.ToMaxHeap();
            Assert.IsTrue(maxHeap.Peek() == array[0], "Wrong maximum.");
        }
    }
}

