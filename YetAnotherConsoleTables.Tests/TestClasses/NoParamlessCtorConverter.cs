using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class NoParamlessCtorConverter
    {
        private NoParamlessCtorConverter() { }

        internal class WithoutParam
        {
#if NET7_0_OR_GREATER
            [TableMemberConverter<TestConverters.NoParamlessCtorConverter>]
#else
            [TableMemberConverter(typeof(TestConverters.NoParamlessCtorConverter))]
#endif
            public string A = "A";
        }

        internal class WithParam
        {
#if NET7_0_OR_GREATER
            [TableMemberConverter<TestConverters.NoParamlessCtorConverter>(ConstructorParams = new object[] { 5 })]
#else
            [TableMemberConverter(typeof(TestConverters.NoParamlessCtorConverter), ConstructorParams = new object[] { 5 })]
#endif
            public string A = "A";
        }

        internal class WithParamOfWrongType
        {
#if NET7_0_OR_GREATER
            [TableMemberConverter<TestConverters.NoParamlessCtorConverter>(ConstructorParams = new object[] { "a" })]
#else
            [TableMemberConverter(typeof(TestConverters.NoParamlessCtorConverter), ConstructorParams = new object[] { "a" })]
#endif
            public string A = "A";
        }
    }
}
