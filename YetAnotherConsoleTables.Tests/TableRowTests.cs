using System;
using System.Linq;
using Xunit;
using YetAnotherConsoleTables.Model;

namespace YetAnotherConsoleTables.Tests
{
    public class TableRowTests
    {
        [Fact]
        public void BaseParsingTest()
        {
            var rowContent = new[]
            {
                ("Column 1", 0),
                ("Column 2", 0),
                ("Column 3", 0)
            };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(1, parsedLines.Count);
            Assert.Equal(rowContent.Select(x => x.Item1), parsedLines[0]);
        }

        [Fact]
        public void MultilineParsingTest()
        {
            var rowContent = new[]
            {
                ($"{Environment.NewLine}Column 1", 0),
                ($"Column 2{Environment.NewLine}Column 2", 0),
                ($"Column 3{Environment.NewLine}", 0)
            };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(2, parsedLines.Count);
            Assert.Equal(new[] { "", "Column 2", "Column 3" }, parsedLines[0]);
            Assert.Equal(new[] { "Column 1", "Column 2", "" }, parsedLines[1]);
        }

        [Fact]
        public void MultilineJaggedParsingTest()
        {
            var rowContent = new[]
            {
                ($"{Environment.NewLine}Column 1", 0),
                ("Column 2", 0)
            };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(2, parsedLines.Count);
            Assert.Equal(new[] { "", "Column 2" }, parsedLines[0]);
            Assert.Equal(new[] { "Column 1", "" }, parsedLines[1]);
        }

        [Fact]
        public void NullRowParsingTest()
        {
            var row = new TableRow(null);

            var parsedLines = row.RowLines;

            Assert.Equal(0, parsedLines.Count);
        }

        [Fact]
        public void NullColumnParsingTest()
        {
            var rowContent = new[]
            {
                ("Column 1", 0),
                (null, 0)
            };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(1, parsedLines.Count);
            Assert.Equal(rowContent.Select(x => x.Item1).Select(x => x != null ? x : ""), parsedLines[0]);
        }
    }
}
