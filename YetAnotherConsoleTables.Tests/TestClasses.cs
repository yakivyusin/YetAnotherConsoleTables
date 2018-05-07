using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Tests
{
    class PropertiesClass
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }
        private string Property3 { get; set; }
    }
    
    class PropertiesSubClass : PropertiesClass
    {
        public string Property4 { get; set; }
    }

    class DisplayNameClass
    {
        [TableMember(DisplayName = "Property 1")]
        public string Property1 { get; set; }
        [TableMember(DisplayName = "Property\r\n2")]
        public string Property2 { get; set; }
    }

    class FieldsClass
    {
        public string Field1;
        public string Field2;
    }

    class EmptyClass
    {
    }

    class IgnoreClass
    {
        public string Property1 { get; set; }
        [TableIgnore]
        public string Property2 { get; set; }
    }

    class OrderedClass
    {
        public string Property3 { get; set; }
        [TableMember(Order = 2)]
        public string Property2 { get; set; }
        [TableMember(Order = 1)]
        public string Property1 { get; set; }
        public string Property4 { get; set; }
    }

    class DefaultValueClass
    {
        [TableMember(DefaultValue = "2")]
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        [TableMember(DefaultValue = null)]
        public string Property3 { get; set; }
    }
}
