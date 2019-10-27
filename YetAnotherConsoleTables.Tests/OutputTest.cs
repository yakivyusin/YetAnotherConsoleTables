using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherConsoleTables.Tests
{
    [TestClass]
    public class OutputTest
    {
        private TextWriter consoleStdOut;

        [TestInitialize]
        public void Setup()
        {
            consoleStdOut = Console.Out;
        }

        [TestCleanup]
        public void Teardown()
        {
            Console.SetOut(consoleStdOut);
        }

        [TestMethod]
        public void DefaultFormatToConsoleTest()
        {
            DefaultFormatTest(true);
        }

        [TestMethod]
        public void DefaultFormatToWriterTest()
        {
            DefaultFormatTest(false);
        }

        public void DefaultFormatTest(bool wrapAsConsole)
        {
            var data = new[]
            {
                new PropertiesClass { Property1 = "AA", Property2 = 3 }
            };
            var table = ConsoleTable.From(data);
            var writer = new Writer();

            if (wrapAsConsole)
            {
                Console.SetOut(writer);
                table.Write();
            }
            else
            {
                table.Write(writer);
            }

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.AreEqual("-------------------------", writer.Values[0]);
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.AreEqual("-------------------------", writer.Values[2]);
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.AreEqual("-------------------------", writer.Values[4]);
        }

        [TestMethod]
        public void PlusFormatTest()
        {
            var data = new[]
            {
                new PropertiesClass { Property1 = "AA", Property2 = 3 }
            };
            var table = ConsoleTable.From(data);
            var writer = new Writer();
            Console.SetOut(writer);

            table.Write(ConsoleTableFormat.Plus);

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.AreEqual("+-----------+-----------+", writer.Values[0]);
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.AreEqual("+-----------+-----------+", writer.Values[2]);
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.AreEqual("+-----------+-----------+", writer.Values[4]);
        }

        [TestMethod]
        public void HeaderFormatTest()
        {
            var data = new[]
            {
                new PropertiesClass { Property1 = "AA", Property2 = 3 }
            };
            var table = ConsoleTable.From(data);
            var writer = new Writer();
            Console.SetOut(writer);

            table.Write(ConsoleTableFormat.Header);

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.AreEqual("|===========|===========|", writer.Values[0]);
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.AreEqual("|===========|===========|", writer.Values[2]);
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.AreEqual("|-----------|-----------|", writer.Values[4]);
        }

        [TestMethod]
        public void FormatWithoitOutsideBordersTest()
        {
            var data = new[]
            {
                new PropertiesClass { Property1 = "AA", Property2 = 3 }
            };
            var table = ConsoleTable.From(data);
            var writer = new Writer();
            Console.SetOut(writer);

            table.Write(new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.HeaderDelimiter | ConsoleTableFormat.Borders.RowDelimiter));

            Assert.AreEqual(3, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 23));
            Assert.AreEqual(" Property1 | Property2 ", writer.Values[0]);
            Assert.AreEqual("-----------------------", writer.Values[1]);
            Assert.AreEqual(" AA        | 3         ", writer.Values[2]);
        }

        private class Writer : TextWriter
        {
            public List<string> Values = new List<string>();

            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            public override void WriteLine(string value)
            {
                base.WriteLine(value);
                Values.Add(value);
            }
        }
    }
}
