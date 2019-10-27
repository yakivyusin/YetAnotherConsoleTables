using System;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Represents ConsoleTable output format.
    /// </summary>
    public partial class ConsoleTableFormat
    {
        /// <summary>
        /// Flags to setup table borders output.
        /// </summary>
        [Flags]
        public enum Borders
        {
            None = 0,
            Top = 1,
            Bottom = 2,
            Left = 4,
            Right = 8,
            HeaderDelimiter = 16,
            RowDelimiter = 32,
            All = 63
        }
    }
}
