using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Instructs the library to use the specified <see cref="TableMemberConverter"/> when converting the member to string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class TableMemberConverterAttribute : Attribute
    {
        private Type converterType;

        public Type ConverterType => converterType;

        public TableMemberConverterAttribute(Type converterType)
        {
            this.converterType = converterType;
        }
    }
}
