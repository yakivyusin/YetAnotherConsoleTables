using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class NullObjectConverter
    {
        [TableMemberConverter(null)]
        public string A = "A";
    }
}