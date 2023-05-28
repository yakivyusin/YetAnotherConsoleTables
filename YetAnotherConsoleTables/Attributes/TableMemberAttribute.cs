using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Sets properties of the table member, such as display name, default value and order.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TableMemberAttribute : Attribute
    {
        private int _order;

        /// <summary>
        /// Instructs the library to always print the member with the specified custom name instead of the member name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Instructs the library to use specified string value if member has null value.
        /// </summary>
        public string DefaultValue { get; set; }

        internal bool IsOrderSpecified { get; private set; }

        /// <summary>
        /// Sets the order of the member in the table.
        /// </summary>
        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                IsOrderSpecified = true;
            }
        }

        /// <summary>
        /// Instructs the library about the minimal width for this member.
        /// <para>If the lengths of all values for this member are less than the set value, they will be padded to this value.</para> 
        /// </summary>
        public int MinWidth { get; set; }
    }
}
