using System.Collections.Generic;
using Xunit;
using YetAnotherConsoleTables.Tests.TestClasses;

namespace YetAnotherConsoleTables.Tests
{
    public class TableContentTests
    {
        [Fact]
        public void BaseTest()
        {
            var collection = new List<Properties>
            {
                new Properties { Property1 = "A", Property2 = 3 },
                new Properties { Property1 = "B", Property2 = 4 }
            };
            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.Equal(2, content.Count);
            Assert.Equal("A", content[0].RowLines[0][0]);
            Assert.Equal("3", content[0].RowLines[0][1]);
            Assert.Equal("B", content[1].RowLines[0][0]);
            Assert.Equal("4", content[1].RowLines[0][1]);
        }

        [Fact]
        public void NullObjectTest()
        {
            var collection = new List<Properties>
            {
                null
            };
            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.Equal(0, content.Count);
        }

        [Fact]
        public void LengthRecalculationTest()
        {
            var collection = new List<Properties>
            {
                new Properties { Property1 = "Property1+", Property2 = 3 }
            };
            var table = ConsoleTable.From(collection);

            Assert.Equal(10, table.ColumnLengths[0]);
        }

        [Fact]
        public void DefaultValueAttrTest()
        {
            var collection = new List<DefaultValue>
            {
                new DefaultValue(),
                new DefaultValue { Property1 = "1" }
            };

            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.Equal(2, content.Count);
            Assert.Equal("2", content[0].RowLines[0][0]);
            Assert.Equal("", content[0].RowLines[0][1]);
            Assert.Equal("1", content[1].RowLines[0][0]);
            Assert.Equal("", content[1].RowLines[0][1]);
        }

        [Fact]
        public void DefaultValueAttrNullTest()
        {
            var collection = new List<DefaultValue>
            {
                new DefaultValue(),
            };

            var table = ConsoleTable.From(collection);

            var content = table.Rows;

            Assert.Equal(1, content.Count);
            Assert.Equal("", content[0].RowLines[0][2]);
        }
    }
}
