using DataStructures.Dictionaries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataStructuresTests
{
    [TestClass]
    public static class CuckooHashTableTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var cuckooTable = new CuckooHashTable<string, int>();

            cuckooTable.Add("Ahmad", 10);
            cuckooTable.Add("Oliver", 11);
            cuckooTable.Add("Konstantinos", 12);
            cuckooTable.Add("Olympos", 13);
            cuckooTable.Add("Bic", 14);
            cuckooTable.Add("Carter", 15);
            cuckooTable.Add("Sameeros", 16);

            var Ahmad = cuckooTable["Ahmad"];
            Assert.IsTrue(Ahmad == 10);

            var Oliver = cuckooTable["Oliver"];
            Assert.IsTrue(Oliver == 11);

            var Konstantinos = cuckooTable["Konstantinos"];
            Assert.IsTrue(Konstantinos == 12);

            var Olympos = cuckooTable["Olympos"];
            Assert.IsTrue(Olympos == 13);

            var Bic = cuckooTable["Bic"];
            Assert.IsTrue(Bic == 14);

            var Carter = cuckooTable["Carter"];
            Assert.IsTrue(Carter == 15);

            var Sameeros = cuckooTable["Sameeros"];
            Assert.IsTrue(Sameeros == 16);

            cuckooTable.Clear();
        }
    }
}
