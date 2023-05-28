using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class Ignore
    {
        public string Property1 { get; set; }

        [TableIgnore]
        public string Property2 { get; set; }
    }
}
