
using Algorithms.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.AlgorithmsTests
{
    [TestClass]
    public static class StringPermutationTests
    {
        [TestMethod]
        public static void DoTest()
        {
            var alphabets = "abcdefg";

            var permutations = Permutations.ComputeDistinct(alphabets);
            Assert.IsTrue(permutations.Count == 720);

            var one = "abcdefg";
            var two = "dabcgfe";
            Assert.IsTrue(Permutations.IsAnargram(one, two) == true);

            one = "123456";
            two = "789123";
            Assert.IsTrue(Permutations.IsAnargram(one, two) == false);

            one = "abc";
            two = "bbb";
            Assert.IsTrue(Permutations.IsAnargram(one, two) == false);

            one = "acdf";
            two = "bcde";
            Assert.IsTrue(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "Legion I am";    // L is capital
            Assert.IsTrue(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "legion I am";    // L is small
            Assert.IsTrue(Permutations.IsAnargram(one, two) == true);
        }
    }
}
