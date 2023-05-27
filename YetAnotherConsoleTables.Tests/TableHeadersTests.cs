using System;
using Xunit;
using YetAnotherConsoleTables.Tests.TestClasses;

namespace YetAnotherConsoleTables.Tests
{
    public class TableHeadersTests
    {
        [Fact]
        public void BaseHeadersPropertiesTest()
        {
            var collection = new[] { new Properties() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(2, headers.ColumnCount);
            Assert.Equal("Property1", headers.RowLines[0][0]);
            Assert.Equal("Property2", headers.RowLines[0][1]);
        }

        [Fact]
        public void SublcassPropertiesTest()
        {
            var collection = new[] { new PropertiesInheritance() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(3, headers.ColumnCount);
            Assert.Equal("Property4", headers.RowLines[0][0]);
            Assert.Equal("Property1", headers.RowLines[0][1]);
            Assert.Equal("Property2", headers.RowLines[0][2]);
        }

        [Fact]
        public void DisplayNameHeadersTest()
        {
            var collection = new[] { new DisplayName() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(2, headers.ColumnCount);
            Assert.Equal("Property 1", headers.RowLines[0][0]);
            Assert.Equal("Property", headers.RowLines[0][1]);
            Assert.Equal("2", headers.RowLines[1][1]);
        }

        [Fact]
        public void BaseHeadersFieldsTest()
        {
            var collection = new[] { new Fields() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(2, headers.ColumnCount);
            Assert.Equal("Field1", headers.RowLines[0][0]);
            Assert.Equal("Field2", headers.RowLines[0][1]);
        }

        [Fact]
        public void IgnoreTest()
        {
            var collection = new[] { new Ignore() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(1, headers.ColumnCount);
            Assert.Equal("Property1", headers.RowLines[0][0]);
        }

        [Fact]
        public void EmptyClassTest()
        {
            var collection = new[] { new Empty() };

            Assert.Throws<InvalidOperationException>(() => ConsoleTable.From(collection));
        }

        [Fact]
        public void NullCollectionTest()
        {
            Assert.Throws<ArgumentNullException>(() => ConsoleTable.From<Properties>(null));
        }

        [Fact]
        public void EmptyCollectionTest()
        {
            var collection = new Properties[0];

            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(2, headers.ColumnCount);
            Assert.Equal("Property1", headers.RowLines[0][0]);
            Assert.Equal("Property2", headers.RowLines[0][1]);
        }

        [Fact]
        public void OrderingTest()
        {
            var collection = new[] { new Ordered() };
            var table = ConsoleTable.From(collection);

            var headers = table.Headers;

            Assert.Equal(4, headers.ColumnCount);
            Assert.Equal("Property1", headers.RowLines[0][0]);
            Assert.Equal("Property2", headers.RowLines[0][1]);
            Assert.Equal("Property3", headers.RowLines[0][2]);
            Assert.Equal("Property4", headers.RowLines[0][3]);
        }
    }
}
