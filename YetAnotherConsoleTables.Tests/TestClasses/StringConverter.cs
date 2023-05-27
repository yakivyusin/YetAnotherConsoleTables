using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class StringConverter
    {
#if NET7_0_OR_GREATER
        [TableMemberConverter<TestConverters.StringConverter>]
#else
        [TableMemberConverter(typeof(TestConverters.StringConverter))]
#endif
        public string A = "A";
    }
}
