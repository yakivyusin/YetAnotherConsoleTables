using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Instructs the library not to print the marked public field or property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableIgnoreAttribute : Attribute
    {
    }
}
