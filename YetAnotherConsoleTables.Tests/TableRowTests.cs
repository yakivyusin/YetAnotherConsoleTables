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
            var rowContent = new[] { "Column 1", "Column 2", "Column 3" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(1, parsedLines.Count);
            Assert.True(rowContent.SequenceEqual(parsedLines[0]));
        }

        [Fact]
        public void MultilineParsingTest()
        {
            var rowContent = new[] { $"{Environment.NewLine}Column 1",
                $"Column 2{Environment.NewLine}Column 2",
                $"Column 3{Environment.NewLine}" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(2, parsedLines.Count);
            Assert.True((new[] { "", "Column 2", "Column 3" }).SequenceEqual(parsedLines[0]));
            Assert.True((new[] { "Column 1", "Column 2", "" }).SequenceEqual(parsedLines[1]));
        }

        [Fact]
        public void MultilineJaggedParsingTest()
        {
            var rowContent = new[] { $"{Environment.NewLine}Column 1", "Column 2" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(2, parsedLines.Count);
            Assert.True((new[] { "", "Column 2" }).SequenceEqual(parsedLines[0]));
            Assert.True((new[] { "Column 1", "" }).SequenceEqual(parsedLines[1]));
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
            var rowContent = new[] { "Column 1", null };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.Equal(1, parsedLines.Count);
            Assert.True(rowContent.Select(x => x != null ? x : "").SequenceEqual(parsedLines[0]));
        }
    }
}
