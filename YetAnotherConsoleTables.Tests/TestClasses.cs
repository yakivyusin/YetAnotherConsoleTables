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
        [TableDisplayName("Property 1")]
        public string Property1 { get; set; }
        [TableDisplayName("Property\r\n2")]
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
        [TableMemberOrder(2)]
        public string Property2 { get; set; }
        [TableMemberOrder(1)]
        public string Property1 { get; set; }
        public string Property4 { get; set; }
    }

    class DefaultValueClass
    {
        [TableDefaultValue("2")]
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        [TableDefaultValue(null)]
        public string Property3 { get; set; }
    }
}
