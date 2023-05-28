using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class PrivateCtorConverter
    {
#if NET7_0_OR_GREATER
        [TableMemberConverter<TestConverters.PrivateCtorConverter>]
#else
        [TableMemberConverter(typeof(TestConverters.PrivateCtorConverter))]
#endif
        public string A = "A";
    }
}
