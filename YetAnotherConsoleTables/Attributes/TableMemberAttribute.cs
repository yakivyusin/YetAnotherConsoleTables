using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Sets properties of the table member, such as display name, default value and order.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableMemberAttribute : Attribute
    {
        private int? order;

        /// <summary>
        /// Instructs the library to always print the member with the specified custom name instead of the member name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Instructs the library to use specified string value if member has null value.
        /// </summary>
        public string DefaultValue { get; set; }

        // It looks some strange, but CLR doesn't support nullable types for attribute properties.
        // Library uses 'null' value to define attribute without setted order.
        // So, attribute has two properties:
        //  * first is public non-nullable property for user to set order;
        //  * second is internal nullable property for library access.

        /// <summary>
        /// Sets the order of the member in the table.
        /// </summary>
        public int Order
        {
            get
            {
                return order ?? default(int);
            }
            set
            {
                order = value;
            }
        }

        internal int? NullableOrder => order;
    }
}
