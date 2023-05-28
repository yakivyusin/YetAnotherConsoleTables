using Xunit;
using YetAnotherConsoleTables.Tests.TestClasses;

namespace YetAnotherConsoleTables.Tests
{
    public class TableMemberConverterTests
    {
        [Fact]
        public void BaseIntTest()
        {
            var data = new[] { new IntConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("+1", row.RowLines[0][0]);
        }

        [Fact]
        public void BaseStringTest()
        {
            var data = new[] { new StringConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("MyConverter:A", row.RowLines[0][0]);
        }

        [Fact]
        public void NoCanConvertTest()
        {
            var data = new[] { new WrongMemberTypeConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("A", row.RowLines[0][0]);
        }

        [Fact]
        public void CanConvertNullTest()
        {
            var data = new[]
            {
                new StringConverter
                {
                    A = null
                }
            };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("MyConverter:", row.RowLines[0][0]);
        }

        [Fact]
        public void NoPublicCtorTest()
        {
            var data = new[] { new PrivateCtorConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("A", row.RowLines[0][0]);
        }

        [Fact]
        public void NoParamlessCtorTest()
        {
            var data = new[] { new NoParamlessCtorConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("A", row.RowLines[0][0]);
        }

        [Fact]
        public void NullTypeTest()
        {
            var data = new[] { new NullObjectConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("A", row.RowLines[0][0]);
        }

        [Fact]
        public void WrongTypeTest()
        {
            var data = new[] { new WrongTypeConverter() };
            var table = ConsoleTable.From(data);

            var row = table.Rows[0];

            Assert.Equal("A", row.RowLines[0][0]);
        }
    }
}
