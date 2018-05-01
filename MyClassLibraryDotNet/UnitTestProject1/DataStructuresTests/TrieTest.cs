using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using DataStructures.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class TrieTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var trie = new Trie();

            // Insert some how to words
            var prefix_howTo = "How to make";

            var word_howToSand = prefix_howTo + " a sandwitch";
            var word_howToRobot = prefix_howTo + " a robot";
            var word_howToOmelet = prefix_howTo + " an omelet";
            var word_howToProp = prefix_howTo + " a proposal";
            var listOfHow = new List<string>() { word_howToSand, word_howToRobot, word_howToOmelet, word_howToProp };

            trie.Add(word_howToOmelet);
            trie.Add(word_howToSand);
            trie.Add(word_howToRobot);
            trie.Add(word_howToProp);

            // Count of words = 4
            Assert.AreEqual(4, trie.Count);

            // Insert some dictionary words
            var prefix_act = "act";

            var word_acts = prefix_act + "s";
            var word_actor = prefix_act + "or";
            var word_acting = prefix_act + "ing";
            var word_actress = prefix_act + "ress";
            var word_active = prefix_act + "ive";
            var listOfActWords = new List<string>() { word_acts, word_actor, word_acting, word_actress, word_active };

            trie.Add(word_actress);
            trie.Add(word_active);
            trie.Add(word_acting);
            trie.Add(word_acts);
            trie.Add(word_actor);

            // Count of words = 9
            Assert.AreEqual(9, trie.Count);

            //
            // ASSERT THE WORDS IN TRIE.

            // Search for a word that doesn't exist
           Assert.IsFalse(trie.ContainsWord(prefix_howTo));

            // Search for prefix
            Assert.IsTrue(trie.ContainsPrefix(prefix_howTo));

            // Search for a prefix using a word
            Assert.IsTrue(trie.ContainsPrefix(word_howToSand));

            // Get all words that start with the how-to prefix
            var someHowToWords = trie.SearchByPrefix(prefix_howTo).ToList();
            Assert.AreEqual(someHowToWords.Count, listOfHow.Count);

            // Assert there are only two words under the prefix "acti" -> active, & acting
            var someActiWords = trie.SearchByPrefix("acti").ToList<string>();
            Assert.IsTrue(someActiWords.Count == 2);
            Assert.IsTrue(someActiWords.Contains(word_acting));
            Assert.IsTrue(someActiWords.Contains(word_active));

            // Assert that "acto" is not a word
            Assert.IsFalse(trie.ContainsWord("acto"));

            // Check the existance of other words
            Assert.IsTrue(trie.ContainsWord(word_actress));
            Assert.IsTrue(trie.ContainsWord(word_howToProp));



            //
            // TEST DELETING SOMETHINGS

            // Removing a prefix should fail
            var removing_acto_fails = false;
            try
            {
                // try removing a non-terminal word
                trie.Remove("acto");
                removing_acto_fails = false;
            }
            catch
            {
                // if exception occured then code works, word doesn't exist.
                removing_acto_fails = true;
            }

            Assert.IsTrue(removing_acto_fails);
            Assert.IsTrue(trie.Count == 9);

            // Removing a word should work
            var removing_acting_passes = false;
            try
            {
                // try removing a non-terminal word
                trie.Remove(word_acting);
                removing_acting_passes = true;
            }
            catch
            {
                // if exception occured then code DOESN'T work, word does exist.
                removing_acting_passes = false;
            }

            Assert.IsTrue(removing_acting_passes);
            Assert.IsTrue(trie.Count == 8);

            someActiWords = trie.SearchByPrefix("acti").ToList<string>();
            Assert.IsTrue(someActiWords.Count == 1);           
            Assert.IsTrue(someActiWords.Contains(word_active));



            //
            // TEST ENUMERATOR

            var enumerator = trie.GetEnumerator();
            var allWords = new List<string>();
            while (enumerator.MoveNext())
                allWords.Add(enumerator.Current);

            // Assert size
            Assert.IsTrue(allWords.Count == trie.Count);

            // Assert each element
            foreach (var word in allWords)
                Debug.Assert(listOfActWords.Contains(word) || listOfHow.Contains(word));
        }
    }
}
