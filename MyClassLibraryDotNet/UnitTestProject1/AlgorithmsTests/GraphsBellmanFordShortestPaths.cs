using System;
using System.Diagnostics;
using System.Collections.Generic;
using Algorithms.Graphs;
using DataStructures.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.AlgorithmsTests
{
    [TestClass]
    public static class GraphsBellmanFordShortestPaths
    {
        [TestMethod]
        public static void DoTest()
        {
            string[] V;
            IEnumerable<WeightedEdge<string>> E;
            DirectedWeightedSparseGraph<string> graph;
            BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string> BellmanFord;

            // Init graph object
            graph = new DirectedWeightedSparseGraph<string>();

            // Init V
            V = new string[6] { "r", "s", "t", "x", "y", "z" };

            // Insert V
            graph.AddVertices(V);
            Assert.IsTrue(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert E
            Assert.IsTrue(graph.AddEdge("r", "s", -3));
            Assert.IsTrue(graph.AddEdge("s", "t", 4));
            Assert.IsTrue(graph.AddEdge("t", "x", -1));
            Assert.IsTrue(graph.AddEdge("x", "y", 6));
            Assert.IsTrue(graph.AddEdge("y", "z", -1));
            Assert.IsTrue(graph.AddEdge("r", "t", 1));
            Assert.IsTrue(graph.AddEdge("s", "x", 7));
            Assert.IsTrue(graph.AddEdge("t", "y", 4));
            Assert.IsTrue(graph.AddEdge("t", "z", 3));
            Assert.IsTrue(graph.AddEdge("x", "z", 2));

            // NEGATIVE CYCLE (BACK-EDGE)
            Assert.IsTrue(graph.AddEdge("y", "t", -7));


            // Get E
            E = graph.Edges;
            Debug.Assert(graph.EdgesCount == 11, "Wrong Edges Count.");

            //
            // PRINT THE GRAPH
            Console.Write("[*] BELLMAN-FORD ON DIRECTED WEIGHTED GRAPH - TEST 01:\r\n");

            Console.WriteLine("Graph representation:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // WILL THROW EXCEPTION
            try
            {
                BellmanFord = new BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "s");
            }
            catch (Exception)
            {
                Assert.IsTrue(graph.RemoveEdge("y", "t"));
                //Debug.Assert(status == true, "Error! Edge was not deleted.");

                BellmanFord = new BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "s");
            }

            //Debug.Assert(graph.HasEdge("y", "t") == false, "Wrong, edge y-t must have been deleted.");

            Debug.Assert(BellmanFord.HasPathTo("r") == false);
            Debug.Assert(BellmanFord.HasPathTo("z") == true);

            // Get shortest path to Z
            var pathToZ = string.Empty;
            foreach (var node in BellmanFord.ShortestPathTo("z"))
                pathToZ = String.Format("{0}({1}) -> ", pathToZ, node);
            pathToZ = pathToZ.TrimEnd(new char[] { ' ', '-', '>' });

            Console.WriteLine("Shortest path to node 'z': " + pathToZ + "\r\n");

            var pathToY = string.Empty;
            foreach (var node in BellmanFord.ShortestPathTo("y"))
                pathToY = String.Format("{0}({1}) -> ", pathToY, node);
            pathToY = pathToY.TrimEnd(new char[] { ' ', '-', '>' });

            Console.WriteLine("Shortest path to node 'y': " + pathToY + "\r\n");

            Console.WriteLine("*********************************************\r\n");


            /***************************************************************************************/


            // Clear the graph and insert new V and E to the instance
            graph.Clear();

            V = new string[] { "s", "a", "b", "c", "d" };

            // Insert new values of V
            graph.AddVertices(V);
            Debug.Assert(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert new value for edges
            Assert.IsTrue(graph.AddEdge("s", "a", 1));
            Assert.IsTrue(graph.AddEdge("a", "b", 1));
            Assert.IsTrue(graph.AddEdge("b", "c", 2));
            Assert.IsTrue(graph.AddEdge("c", "a", -5));
            Assert.IsTrue(graph.AddEdge("c", "d", 2));

            Debug.Assert(graph.EdgesCount == 5, "Wrong Edges Count.");

            // PRINT THE GRAPH
            Console.Write("[*] BELLMAN-FORD ON DIRECTED WEIGHTED GRAPH - TEST 01:\r\n");

            Console.WriteLine("Graph representation:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // WILL THROW EXCEPTION
            try
            {
                BellmanFord = new BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "b");
            }
            catch (Exception)
            {
                Assert.IsTrue(graph.RemoveEdge("c", "a"));
                //Debug.Assert(status == true, "Error! Edge was not deleted.");

                BellmanFord = new BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "b");
            }

            Console.WriteLine("*********************************************\r\n");


            /***************************************************************************************/


            // Clear the graph and insert new V and E to the instance
            graph.Clear();

            V = new string[] { "A", "B", "C", "D", "E" };

            // Insert new values of V
            graph.AddVertices(V);
            Debug.Assert(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert new value for edges
            Assert.IsTrue(graph.AddEdge("A", "C", 7));
            Assert.IsTrue(graph.AddEdge("B", "A", 19));
            Assert.IsTrue(graph.AddEdge("B", "C", 11));
            Assert.IsTrue(graph.AddEdge("C", "E", 5));
            Assert.IsTrue(graph.AddEdge("C", "D", 15));
            Assert.IsTrue(graph.AddEdge("D", "B", 4));
            Assert.IsTrue(graph.AddEdge("E", "D", 13));

            Debug.Assert(graph.EdgesCount == 7, "Wrong Edges Count.");

            //
            // PRINT THE GRAPH
            Console.Write("[*] BELLMAN-FORD ON DIRECTED WEIGHTED GRAPH - TEST 01:\r\n");

            Console.WriteLine("Graph representation:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // Init BELLMAN-FORD
            BellmanFord = new BellmanFordShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "A");

            var pathToD = string.Empty;
            foreach (var node in BellmanFord.ShortestPathTo("D"))
                pathToD = String.Format("{0}({1}) -> ", pathToD, node);
            pathToD = pathToD.TrimEnd(new char[] { ' ', '-', '>' });

            Console.WriteLine("Shortest path from 'A' to 'D': " + pathToD + "\r\n");

            Console.WriteLine("*********************************************\r\n");


            /***************************************************************************************/


            //var dijkstraAllPairs = new DijkstraAllPairsShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph);

            //var vertices = graph.Vertices;

            //Console.WriteLine("Dijkstra All Pairs Shortest Paths: \r\n");

            //foreach (var source in vertices)
            //{
            //    foreach (var destination in vertices)
            //    {
            //        var shortestPath = string.Empty;
            //        foreach (var node in dijkstraAllPairs.ShortestPath(source, destination))
            //            shortestPath = String.Format("{0}({1}) -> ", shortestPath, node);

            //        shortestPath = shortestPath.TrimEnd(new char[] { ' ', '-', '>' });

            //        Console.WriteLine("Shortest path from '" + source + "' to '" + destination + "' is: " + shortestPath + "\r\n");
            //    }
            //}

            Console.ReadLine();
        }

    }
}
