using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class IntConverter
    {
#if NET7_0_OR_GREATER
        [TableMemberConverter<TestConverters.IntConverter>]
#else
        [TableMemberConverter(typeof(TestConverters.IntConverter))]
#endif
        public int A = 1;
    }
}
