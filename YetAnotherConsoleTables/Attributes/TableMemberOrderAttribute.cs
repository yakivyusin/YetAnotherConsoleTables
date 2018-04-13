using System;

namespace YetAnotherConsoleTables.Attributes
{
    /// <summary>
    /// Sets the order of the member in the table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true)]
    public class TableMemberOrderAttribute : Attribute
    {
        private int order;

        public int Order => order;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableMemberOrderAttribute"/> with the specified order.
        /// </summary>
        /// <param name="order">Order of the member in the table.</param>
        public TableMemberOrderAttribute(int order)
        {
            this.order = order;
        }
    }
}
