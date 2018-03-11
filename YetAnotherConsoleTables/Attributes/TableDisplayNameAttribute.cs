using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Instructs the library to always print the member with the specified custom name instead of the member name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableDisplayNameAttribute : Attribute
    {
        private string name;
        
        public string Name => name != null ? name : "";

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDisplayNameAttribute"/> with the specified name.
        /// </summary>
        /// <param name="name">Name of the property of field.</param>
        public TableDisplayNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
