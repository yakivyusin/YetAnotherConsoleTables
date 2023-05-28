using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class NoParamlessCtorConverter
    {
#if NET7_0_OR_GREATER
        [TableMemberConverter<TestConverters.NoParamlessCtorConverter>]
#else
        [TableMemberConverter(typeof(TestConverters.NoParamlessCtorConverter))]
#endif
        public string A = "A";
    }
}
