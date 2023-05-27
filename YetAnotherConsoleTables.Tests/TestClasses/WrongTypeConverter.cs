using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class WrongTypeConverter
    {
        [TableMemberConverter(typeof(WrongTypeConverter))]
        public string A = "A";
    }
}
