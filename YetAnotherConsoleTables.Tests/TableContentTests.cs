using System;
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

        [Fact]
        public void MinWidthAttrTest()
        {
            var collection = new List<MinWidth>
            {
                new MinWidth { Property = null },
                new MinWidth { Property = "5 < 9" },
                new MinWidth { Property = "9nine = 9" },
                new MinWidth { Property = "1thirteen > 9" }
            };

            var table = ConsoleTable.From(collection);
            var content = table.Rows;

            Assert.Equal(4, content.Count);
            Assert.Equal("         ", content[0].RowLines[0][0]);
            Assert.Equal("5 < 9    ", content[1].RowLines[0][0]);
            Assert.Equal("9nine = 9", content[2].RowLines[0][0]);
            Assert.Equal("1thirteen > 9", content[3].RowLines[0][0]);
        }

        [Fact]
        public void MinWidthAttrMultilineTest()
        {
            var collection = new List<MinWidth>
            {
                new MinWidth { Property = $"5 < 9{Environment.NewLine}11 > 9" }
            };

            var table = ConsoleTable.From(collection);
            var content = table.Rows;

            Assert.Equal(1, content.Count);
            Assert.Equal("5 < 9    ", content[0].RowLines[0][0]);
            Assert.Equal("11 > 9   ", content[0].RowLines[1][0]);
        }
    }
}
