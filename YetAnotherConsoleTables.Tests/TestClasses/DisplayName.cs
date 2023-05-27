using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests.TestClasses
{
    internal class DisplayName
    {
        [TableMember(DisplayName = "Property 1")]
        public string Property1 { get; set; }

        [TableMember(DisplayName = "Property\r\n2")]
        public string Property2 { get; set; }
    }
}
