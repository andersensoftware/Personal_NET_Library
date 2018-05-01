using System;
using System.Linq;
using DataStructures.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class GraphsUndirectedWeightedDenseGraphTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();

            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };

            graph.AddVertices(verticesSet1);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);
            graph.AddEdge("s", "x", 3);
            graph.AddEdge("x", "d", 1);
            graph.AddEdge("x", "c", 2);
            graph.AddEdge("x", "a", 3);
            graph.AddEdge("d", "f", 1);
            graph.AddEdge("d", "c", 2);
            graph.AddEdge("d", "s", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("c", "v", 2);

            var allEdges = graph.Edges.ToList();

            Assert.IsTrue(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 11, "Wrong edges count.");
            Assert.IsTrue(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.IsTrue(graph.OutgoingEdges("a").ToList().Count == 3, "Wrong outgoing edges from 'a'.");
            Assert.IsTrue(graph.OutgoingEdges("s").ToList().Count == 3, "Wrong outgoing edges from 's'.");
            Assert.IsTrue(graph.OutgoingEdges("x").ToList().Count == 4, "Wrong outgoing edges from 'x'.");
            Assert.IsTrue(graph.OutgoingEdges("d").ToList().Count == 4, "Wrong outgoing edges from 'd'.");
            Assert.IsTrue(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Assert.IsTrue(graph.OutgoingEdges("v").ToList().Count == 1, "Wrong outgoing edges from 'v'.");
            Assert.IsTrue(graph.OutgoingEdges("f").ToList().Count == 2, "Wrong outgoing edges from 'f'.");
            Assert.IsTrue(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Assert.IsTrue(graph.IncomingEdges("a").ToList().Count == 3, "Wrong incoming edges from 'a'.");
            Assert.IsTrue(graph.IncomingEdges("s").ToList().Count == 3, "Wrong incoming edges from 's'.");
            Assert.IsTrue(graph.IncomingEdges("x").ToList().Count == 4, "Wrong incoming edges from 'x'.");
            Assert.IsTrue(graph.IncomingEdges("d").ToList().Count == 4, "Wrong incoming edges from 'd'.");
            Assert.IsTrue(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Assert.IsTrue(graph.IncomingEdges("v").ToList().Count == 1, "Wrong incoming edges from 'v'.");
            Assert.IsTrue(graph.IncomingEdges("f").ToList().Count == 2, "Wrong incoming edges from 'f'.");
            Assert.IsTrue(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            var f_to_c = graph.HasEdge("f", "c");
            var f_to_c_weight = graph.GetEdgeWeight("f", "c");
            Assert.IsTrue(f_to_c == true, "Edge f->c doesn't exist.");
            Assert.IsTrue(f_to_c_weight == 1, "Edge f->c must have a weight of 1.");

            var d_to_s = graph.HasEdge("d", "s");
            var d_to_s_weight = graph.GetEdgeWeight("d", "s");
            Assert.IsTrue(d_to_s == true, "Edge d->s doesn't exist.");
            Assert.IsTrue(d_to_s_weight == 3, "Edge d->s must have a weight of 3.");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            Assert.IsTrue(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 8, "Wrong edges count.");

            graph.RemoveVertex("x");
            Assert.IsTrue(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 4, "Wrong edges count.");

            graph.AddVertex("x");
            graph.AddEdge("s", "x", 3);
            graph.AddEdge("x", "d", 1);
            graph.AddEdge("x", "c", 2);
            graph.AddEdge("x", "a", 3);
            graph.AddEdge("d", "c", 2);
            graph.AddEdge("c", "v", 2);
            graph.AddEdge("a", "z", 2);

            // BFS from A
            // Walk the graph using BFS from A:
            var bfsWalk = graph.BreadthFirstWalk("a");

            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.IsTrue(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // DFS from A
            // Walk the graph using DFS from A:
            var dfsWalk = graph.DepthFirstWalk("a");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.IsTrue(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // BFS from F
            // Walk the graph using BFS from F:
            bfsWalk = graph.BreadthFirstWalk("f");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.IsTrue(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // DFS from F
            // Walk the graph using DFS from F:
            dfsWalk = graph.DepthFirstWalk("f");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.IsTrue(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));


            /********************************************************************/

            graph.Clear();

            var verticesSet2 = new string[] { "a", "b", "c", "d", "e", "f" };

            graph.AddVertices(verticesSet2);

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("a", "d", 2);
            graph.AddEdge("b", "e", 3);
            graph.AddEdge("d", "b", 1);
            graph.AddEdge("d", "e", 2);
            graph.AddEdge("e", "c", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("f", "f", 1);

            Assert.IsTrue(graph.VerticesCount == 6, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 8, "Wrong edges count.");

            // Walk the graph using DFS:
            dfsWalk = graph.DepthFirstWalk();
            // output: (a) (b) (e) (d) (c) (f) 
            Assert.IsTrue(bfsWalk.SequenceEqual(new[] { "a", "b", "e", "d", "c", "f" }));

            Console.ReadLine();
        }

    }

}
