using Microsoft.VisualStudio.TestTools.UnitTesting;
using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests
{
    [TestClass]
    public class TableMemberConverterTest
    {
        [TestMethod]
        public void BaseIntTest()
        {
            var data = new[] { new SuccessObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("+1", row.RowLines[0][0]);
        }

        [TestMethod]
        public void BaseStringTest()
        {
            var data = new[] { new SuccessGenericObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("MyConverter:A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NoCanConvertTest()
        {
            var data = new[] { new NoCanConvertObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void CanConvertNullTest()
        {
            var data = new[] { new CanConvertNullRefObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("MyConverter:", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NoPublicCtorTest()
        {
            var data = new[] { new NoPublicCtorObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NoParamlessCtorTest()
        {
            var data = new[] { new NoParamlessCtorObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NullTypeTest()
        {
            var data = new[] { new NullTypeObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NoCallStaticTest()
        {
            var data = new[] { new StaticParamlessObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        [TestMethod]
        public void NoJsonConverterTypeTest()
        {
            var data = new[] { new NoJsonConverterTypeObject() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.AreEqual("A", row.RowLines[0][0]);
        }

        private class SuccessObject
        {
            [TableMemberConverter(typeof(BaseIntConverter))]
            public int A = 1;
        }

        private class SuccessGenericObject
        {
            [TableMemberConverter(typeof(BaseStringConverter))]
            public string A = "A";
        }

        private class NoCanConvertObject
        {
            [TableMemberConverter(typeof(BaseIntConverter))]
            public string A = "A";
        }

        private class CanConvertNullRefObject
        {
            [TableMemberConverter(typeof(BaseStringConverter))]
            public string A = null;
        }

        private class NoPublicCtorObject
        {
            [TableMemberConverter(typeof(NoPublicCtor))]
            public string A = "A";
        }

        private class NoParamlessCtorObject
        {
            [TableMemberConverter(typeof(NoParamlessCtor))]
            public string A = "A";
        }

        private class NullTypeObject
        {
            [TableMemberConverter(null)]
            public string A = "A";
        }

        private class StaticParamlessObject
        {
            [TableMemberConverter(typeof(StaticParamless))]
            public string A = "A";
        }

        private class NoJsonConverterTypeObject
        {
            [TableMemberConverter(typeof(StaticParamlessObject))]
            public string A = "A";
        }
    }
}
