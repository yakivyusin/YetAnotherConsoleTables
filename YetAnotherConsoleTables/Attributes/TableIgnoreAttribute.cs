using System;

namespace YetAnotherConsoleTables.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableIgnoreAttribute : Attribute
    {
    }
}
