using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class SortedDictionaryTests
    {
        [TestMethod]
        public static void DoTest()
        {
            var sortedDict = new DataStructures.SortedCollections.SortedDictionary<string, int>();

            string[] keys = new string[13] {
                "A", "B", "C", "D", "E", "ABC", "Ahmad", "Bic",
                "Carter", "Konstantinos", "Olympos", "Tareq", "Ziad"
            };

            int[] values = new int[13] {
                26, 27, 28, 29, 30, 40, 10, 11,
                12, 13, 14, 15, 16
            };

            //
            // Test Add
            for (int i = 0; i < 13; ++i)
            {
                // insert
                sortedDict.Add(keys[i], values[i]);
            }

            //
            // Assert correct number of elements
            Assert.IsTrue(sortedDict.Count == 13, "Wrong number of elements in dictionary.");

            //
            // Test get via index-access notation
            Assert.IsTrue(sortedDict["A"] == 26);
            Assert.IsTrue(sortedDict["B"] == 27);
            Assert.IsTrue(sortedDict["C"] == 28);
            Assert.IsTrue(sortedDict["D"] == 29);
            Assert.IsTrue(sortedDict["E"] == 30);
            Assert.IsTrue(sortedDict["ABC"] == 40);
            Assert.IsTrue(sortedDict["Ahmad"] == 10);
            Assert.IsTrue(sortedDict["Bic"] == 11);
            Assert.IsTrue(sortedDict["Carter"] == 12);
            Assert.IsTrue(sortedDict["Konstantinos"] == 13);
            Assert.IsTrue(sortedDict["Olympos"] == 14);
            Assert.IsTrue(sortedDict["Tareq"] == 15);
            Assert.IsTrue(sortedDict["Ziad"] == 16);

            //
            // Test update
            int bak1 = sortedDict["Ahmad"];
            int bak2 = sortedDict["ABC"];
            sortedDict["Ahmad"] = 100;
            sortedDict["ABC"] = 200;

            Assert.IsTrue(sortedDict["ABC"] == 200, "Expcted ABC to be set to 200.");
            Assert.IsTrue(sortedDict["Ahmad"] == 100, "Expected Ahmad to be set to 100.");

            // Restore
            sortedDict["Ahmad"] = bak1;
            sortedDict["ABC"] = bak2;

            //
            // Test TryGetValue for existing items
            int existingItemKeyValue;
            var tryGetStatus = sortedDict.TryGetValue("Ziad", out existingItemKeyValue);
            Assert.IsTrue(tryGetStatus, "Expected the TryGet returned status to be true.");
            Assert.IsTrue(existingItemKeyValue == 16, "Expected Ziad to be set to 16.");

            //
            // Test TryGetValue for non-existing items
            int nonExistingItemKeyValue;
            tryGetStatus = sortedDict.TryGetValue("SomeNonExistentKey", out nonExistingItemKeyValue);
           Assert.IsFalse(tryGetStatus, "Expected the TryGet returned status to be false.");
            Assert.IsTrue(existingItemKeyValue == 16, "Expected the returned value for a non-existent key to be 0.");

            //
            // Test Remove
            var previousCount = sortedDict.Count;
            var removeStatus = sortedDict.Remove("Ziad");
            Assert.IsTrue(removeStatus, "Expected removeStatus to be true.");
           Assert.IsFalse(sortedDict.ContainsKey("Ziad"), "Expected Ziad to be removed.");
            Assert.IsTrue(sortedDict.Count == previousCount - 1, "Expected Count to decrease after Remove operation.");

            //
            // Test CopyTo returns a sorted array of key-value pairs (sorted by key).
            var array = new KeyValuePair<string, int>[sortedDict.Count];
            sortedDict.CopyTo(array, 0);

            // Prepare the sort testing data
            var keyValuePairsList = new List<KeyValuePair<string, int>>(sortedDict.Count);
            for (int i = 0; i < sortedDict.Count; ++i)
            {
                if (keys[i] == "Ziad") // deleted previously from sortedDictionary
                    continue;
                keyValuePairsList.Add(new KeyValuePair<string, int>(keys[i], values[i]));
            }

            // Sort dictionary
            keyValuePairsList = keyValuePairsList.OrderBy(item => item.Key, new KeyComparer()).ToList();

            // begin sorting test
            for (int i = 0; i < sortedDict.Count; i++)
            {
                // Keys
                string key1 = array[i].Key;
                string key2 = keyValuePairsList[i].Key;

                // Values
                int val1 = array[i].Value;
                int val2 = keyValuePairsList[i].Value;

                Assert.IsTrue(key1.Equals(key2, System.StringComparison.Ordinal), "Unmatched order of items!");
                Assert.AreEqual(val1, val2);
            }

            //
            // Test Clear
            sortedDict.Clear();
            Assert.IsTrue(sortedDict.Count == 0, "Expected sortedDict to be empty!");
        }


        private class KeyComparer : IComparer<string>
        {
            public int Compare(string x, string y) => string.CompareOrdinal(x, y);
        }
    }

}

