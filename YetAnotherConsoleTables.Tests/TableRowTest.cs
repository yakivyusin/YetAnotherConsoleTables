using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YetAnotherConsoleTables.Model;

namespace YetAnotherConsoleTables.Tests
{
    [TestClass]
    public class TableRowTest
    {
        [TestMethod]
        public void BaseParsingTest()
        {
            var rowContent = new[] { "Column 1", "Column 2", "Column 3" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.AreEqual(1, parsedLines.Count);
            Assert.IsTrue(rowContent.SequenceEqual(parsedLines[0]));
        }

        [TestMethod]
        public void MultilineParsingTest()
        {
            var rowContent = new[] { $"{Environment.NewLine}Column 1",
                $"Column 2{Environment.NewLine}Column 2",
                $"Column 3{Environment.NewLine}" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.AreEqual(2, parsedLines.Count);
            Assert.IsTrue((new[] { "", "Column 2", "Column 3" }).SequenceEqual(parsedLines[0]));
            Assert.IsTrue((new[] { "Column 1", "Column 2", "" }).SequenceEqual(parsedLines[1]));
        }

        [TestMethod]
        public void MultilineJaggedParsingTest()
        {
            var rowContent = new[] { $"{Environment.NewLine}Column 1", "Column 2" };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.AreEqual(2, parsedLines.Count);
            Assert.IsTrue((new[] { "", "Column 2" }).SequenceEqual(parsedLines[0]));
            Assert.IsTrue((new[] { "Column 1","" }).SequenceEqual(parsedLines[1]));
        }

        [TestMethod]
        public void NullRowParsingTest()
        {
            var row = new TableRow(null);

            var parsedLines = row.RowLines;

            Assert.AreEqual(0, parsedLines.Count);
        }

        [TestMethod]
        public void NullColumnParsingTest()
        {
            var rowContent = new[] { "Column 1", null };
            var row = new TableRow(rowContent);

            var parsedLines = row.RowLines;

            Assert.AreEqual(1, parsedLines.Count);
            Assert.IsTrue(rowContent.Select(x => x != null ? x : "").SequenceEqual(parsedLines[0]));
        }
    }
}
