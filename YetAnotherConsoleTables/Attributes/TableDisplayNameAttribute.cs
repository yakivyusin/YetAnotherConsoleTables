using System;

namespace YetAnotherConsoleTables.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableDisplayNameAttribute : Attribute
    {
        private string name;
        
        public string Name => name != null ? name : "";

        public TableDisplayNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
