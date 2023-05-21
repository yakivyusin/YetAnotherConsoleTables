using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Instructs the library to use the specified <see cref="TableMemberConverter"/> when converting the member to string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
#if NET7_0_OR_GREATER
    [Obsolete("For .NET 7+, TableMemberConverterAttribute is obsolete and will be removed in future releases. Please use TableMemberConverterAttribute<T> instead.")]
#endif
    public class TableMemberConverterAttribute : Attribute
    {
        internal Type ConverterType { get; }

        public TableMemberConverterAttribute(Type converterType) => ConverterType = converterType;
    }

#if NET7_0_OR_GREATER
    /// <summary>
    /// Instructs the library to use the specified <typeparamref name="TConverter"/> when converting the member to string.
    /// </summary>
    public class TableMemberConverterAttribute<TConverter> : TableMemberConverterAttribute where TConverter : TableMemberConverter
    {
        public TableMemberConverterAttribute() : base(typeof(TConverter))
        {
        }
    }
#endif
}
