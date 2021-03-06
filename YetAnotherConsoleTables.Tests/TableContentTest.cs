﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var table = ConsoleTable.From(collection);

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
            var table = ConsoleTable.From(collection);

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
            var table = ConsoleTable.From(collection);

            Assert.AreEqual(10, table.ColumnLengths[0]);
        }

        [TestMethod]
        public void DefaultValueAttrTest()
        {
            var collection = new List<DefaultValueClass>
            {
                new DefaultValueClass(),
                new DefaultValueClass { Property1 = "1" }
            };

            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("2", content[0].RowLines[0][0]);
            Assert.AreEqual("", content[0].RowLines[0][1]);
            Assert.AreEqual("1", content[1].RowLines[0][0]);
            Assert.AreEqual("", content[1].RowLines[0][1]);
        }

        [TestMethod]
        public void DefaultValueAttrNullTest()
        {
            var collection = new List<DefaultValueClass>
            {
                new DefaultValueClass(),
            };

            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("", content[0].RowLines[0][2]);
        }
    }
}
