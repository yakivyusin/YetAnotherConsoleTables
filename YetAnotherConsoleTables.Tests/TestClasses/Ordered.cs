using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class Ordered
    {
        public string Property3 { get; set; }

        [TableMember(Order = 2)]
        public string Property2 { get; set; }

        [TableMember(Order = 1)]
        public string Property1 { get; set; }
        public string Property4 { get; set; }
    }
}
