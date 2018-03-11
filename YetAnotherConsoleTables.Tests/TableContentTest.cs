using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace YetAnotherConsoleTables.Tests
{
    [TestClass]
    public class TableContentTest
    {
        [TestMethod]
        public void BaseTest()
        {
            var collection = new List<PropertiesClass>
            {
                new PropertiesClass { Property1 = "A", Property2 = 3 },
                new PropertiesClass { Property1 = "B", Property2 = 4 }
            };
            var table = ConsoleTables.From(collection);

            var content = table.Rows;

            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("A", content[0].RowLines[0][0]);
            Assert.AreEqual("3", content[0].RowLines[0][1]);
            Assert.AreEqual("B", content[1].RowLines[0][0]);
            Assert.AreEqual("4", content[1].RowLines[0][1]);
        }

        [TestMethod]
        public void NullObjectTest()
        {
            var collection = new List<PropertiesClass>
            {
                null
            };
            var table = ConsoleTables.From(collection);

            var content = table.Rows;

            Assert.AreEqual(0, content.Count);
        }

        [TestMethod]
        public void LengthRecalculationTest()
        {
            var collection = new List<PropertiesClass>
            {
                new PropertiesClass { Property1 = "Property1+", Property2 = 3 }
            };
            var table = ConsoleTables.From(collection);

            Assert.AreEqual(10, table.ColumnLengths[0]);
        }
    }
}
