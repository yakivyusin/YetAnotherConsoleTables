using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class MinWidth
    {
        [TableMember(MinWidth = 9)]
        public string Property { get; set; }
    }
}
