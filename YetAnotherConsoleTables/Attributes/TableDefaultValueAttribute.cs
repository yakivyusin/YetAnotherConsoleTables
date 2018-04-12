using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Instructs the library to use specified string value for null member.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableDefaultValueAttribute : Attribute
    {
        private string value;

        public string Value => value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefaultValueAttribute"/> with the specified value.
        /// </summary>
        /// <param name="value">Default value of the property or field if it's null.</param>
        public TableDefaultValueAttribute(string value)
        {
            this.value = value;
        }
    }
}
