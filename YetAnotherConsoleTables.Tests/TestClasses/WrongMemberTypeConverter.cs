using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class WrongMemberTypeConverter
    {
#if NET7_0_OR_GREATER
        [TableMemberConverter<TestConverters.IntConverter>]
#else
        [TableMemberConverter(typeof(TestConverters.IntConverter))]
#endif
        public string A = "A";
    }
}
