﻿using System.Linq;
using DataStructures.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class GraphsUndirectedSparseGraphTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var graph = new UndirectedSparseGraph<string>();

            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };

            graph.AddVertices(verticesSet1);

            graph.AddEdge("a", "s");
            graph.AddEdge("a", "z");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "f");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");

            var allEdges = graph.Edges.ToList();

            Assert.IsTrue(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 10, "Wrong edges count.");
            Assert.IsTrue(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.IsTrue(graph.OutgoingEdges("a").ToList().Count == 2, "Wrong outgoing edges from 'a'.");
            Assert.IsTrue(graph.OutgoingEdges("s").ToList().Count == 2, "Wrong outgoing edges from 's'.");
            Assert.IsTrue(graph.OutgoingEdges("x").ToList().Count == 3, "Wrong outgoing edges from 'x'.");
            Assert.IsTrue(graph.OutgoingEdges("d").ToList().Count == 3, "Wrong outgoing edges from 'd'.");
            Assert.IsTrue(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Assert.IsTrue(graph.OutgoingEdges("v").ToList().Count == 2, "Wrong outgoing edges from 'v'.");
            Assert.IsTrue(graph.OutgoingEdges("f").ToList().Count == 3, "Wrong outgoing edges from 'f'.");
            Assert.IsTrue(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Assert.IsTrue(graph.IncomingEdges("a").ToList().Count == 2, "Wrong incoming edges from 'a'.");
            Assert.IsTrue(graph.IncomingEdges("s").ToList().Count == 2, "Wrong incoming edges from 's'.");
            Assert.IsTrue(graph.IncomingEdges("x").ToList().Count == 3, "Wrong incoming edges from 'x'.");
            Assert.IsTrue(graph.IncomingEdges("d").ToList().Count == 3, "Wrong incoming edges from 'd'.");
            Assert.IsTrue(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Assert.IsTrue(graph.IncomingEdges("v").ToList().Count == 2, "Wrong incoming edges from 'v'.");
            Assert.IsTrue(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Assert.IsTrue(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            Assert.IsTrue(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 7, "Wrong edges count.");


            graph.RemoveVertex("x");
            Assert.IsTrue(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 4, "Wrong edges count.");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");

            Assert.IsTrue(graph.BreadthFirstWalk("s").SequenceEqual(new string[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            graph.AddVertices(new string[] { "a", "b", "c", "d", "e", "f" });

            graph.AddEdge("a", "b");
            graph.AddEdge("a", "d");
            graph.AddEdge("b", "e");
            graph.AddEdge("d", "b");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("f", "f");

            Assert.IsTrue(graph.VerticesCount == 10, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 17, "Wrong edges count.");

            Assert.IsTrue(graph.DepthFirstWalk().SequenceEqual(new string[] { "a", "d", "e", "c", "v", "f", "x", "s", "b", "z" }));
        }
    }
}

