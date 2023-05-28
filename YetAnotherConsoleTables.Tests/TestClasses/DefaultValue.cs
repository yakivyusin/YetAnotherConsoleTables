using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class DefaultValue
    {
        [TableMember(DefaultValue = "2")]
        public string Property1 { get; set; }

        public string Property2 { get; set; }

        [TableMember(DefaultValue = null)]
        public string Property3 { get; set; }
    }
}
