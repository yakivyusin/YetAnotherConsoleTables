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
        [TestMethod]
        public void DefaultFormatTest()
        {
            var data = new[]
            {
                new PropertiesClass { Property1 = "AA", Property2 = 3 }
            };
            var table = ConsoleTable.From(data);
            var writer = new Writer();
            var oldConsoleOut = Console.Out;
            Console.SetOut(writer);

            table.Write();

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.IsTrue(writer.Values[0].All(x => x == '-'));
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.IsTrue(writer.Values[2].All(x => x == '-'));
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.IsTrue(writer.Values[4].All(x => x == '-'));

            Console.SetOut(oldConsoleOut);
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
            var oldConsoleOut = Console.Out;
            Console.SetOut(writer);

            table.Write(ConsoleTableFormat.Plus);

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.AreEqual("+-----------+-----------+", writer.Values[0]);
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.AreEqual("+-----------+-----------+", writer.Values[2]);
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.AreEqual("+-----------+-----------+", writer.Values[4]);

            Console.SetOut(oldConsoleOut);
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
            var oldConsoleOut = Console.Out;
            Console.SetOut(writer);

            table.Write(ConsoleTableFormat.Header);

            Assert.AreEqual(5, writer.Values.Count);
            Assert.IsTrue(writer.Values.All(x => x.Length == 25));
            Assert.AreEqual("|===========|===========|", writer.Values[0]);
            Assert.AreEqual("| Property1 | Property2 |", writer.Values[1]);
            Assert.AreEqual("|===========|===========|", writer.Values[2]);
            Assert.AreEqual("| AA        | 3         |", writer.Values[3]);
            Assert.AreEqual("|-----------|-----------|", writer.Values[4]);

            Console.SetOut(oldConsoleOut);
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
