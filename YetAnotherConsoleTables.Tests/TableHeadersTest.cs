﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YetAnotherConsoleTables.Tests
{
    [TestClass]
    public class TableHeadersTest
    {
        [TestMethod]
        public void BaseHeadersPropertiesTest()
        {
            var collection = new[] { new PropertiesClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(2, headers.ColumnCount);
            Assert.AreEqual("Property1", headers.RowLines[0][0]);
            Assert.AreEqual("Property2", headers.RowLines[0][1]);
        }

        [TestMethod]
        public void SublcassPropertiesTest()
        {
            var collection = new[] { new PropertiesSubClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(3, headers.ColumnCount);
            Assert.AreEqual("Property4", headers.RowLines[0][0]);
            Assert.AreEqual("Property1", headers.RowLines[0][1]);
            Assert.AreEqual("Property2", headers.RowLines[0][2]);
        }

        [TestMethod]
        public void DisplayNameHeadersTest()
        {
            var collection = new[] { new DisplayNameClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(2, headers.ColumnCount);
            Assert.AreEqual("Property 1", headers.RowLines[0][0]);
            Assert.AreEqual("Property", headers.RowLines[0][1]);
            Assert.AreEqual("2", headers.RowLines[1][1]);
        }

        [TestMethod]
        public void BaseHeadersFieldsTest()
        {
            var collection = new[] { new FieldsClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(2, headers.ColumnCount);
            Assert.AreEqual("Field1", headers.RowLines[0][0]);
            Assert.AreEqual("Field2", headers.RowLines[0][1]);
        }

        [TestMethod]
        public void IgnoreTest()
        {
            var collection = new[] { new IgnoreClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(1, headers.ColumnCount);
            Assert.AreEqual("Property1", headers.RowLines[0][0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyClassTest()
        {
            var collection = new[] { new EmptyClass() };

            var table = ConsoleTable.From(collection);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullCollectionTest()
        {
            var table = ConsoleTable.From<PropertiesClass>(null);
        }

        [TestMethod]
        public void EmptyCollectionTest()
        {
            var collection = new PropertiesClass[0];

            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(2, headers.ColumnCount);
            Assert.AreEqual("Property1", headers.RowLines[0][0]);
            Assert.AreEqual("Property2", headers.RowLines[0][1]);
        }

        [TestMethod]
        public void OrderingTest()
        {
            var collection = new[] { new OrderedClass() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.AreEqual(4, headers.ColumnCount);
            Assert.AreEqual("Property1", headers.RowLines[0][0]);
            Assert.AreEqual("Property2", headers.RowLines[0][1]);
            Assert.AreEqual("Property3", headers.RowLines[0][2]);
            Assert.AreEqual("Property4", headers.RowLines[0][3]);
        }
    }
}
