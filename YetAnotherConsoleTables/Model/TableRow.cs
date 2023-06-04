using System;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherConsoleTables.Model
{
    /// <summary>
    /// Represents a table row that can contains multiple lines.
    /// </summary>
    internal class TableRow
    {
        private readonly List<string[]> _rowLines = new List<string[]>();

        internal int ColumnCount { get; private set; }
        internal IReadOnlyList<string[]> RowLines => _rowLines.AsReadOnly();

        internal TableRow((string value, int minWidth)[] row)
        {
            SplitRow(row);
        }

        private void SplitRow((string value, int minWidth)[] row)
        {
            if (row == null)
            {
                return;
            }

            var splittedLines = row.Select(column => PadStrings(SplitString(column.value), column.minWidth)).ToList();
            var linesCount = splittedLines.Max(column => column.Length);

            for (int i = 0; i < linesCount; i++)
            {
                _rowLines.Add(splittedLines.Select(x => i >= x.Length ? string.Empty : x[i]).ToArray());
            }

            ColumnCount = row.Length;
        }

        private string[] SplitString(string input)
        {
            if (input == null)
            {
                return new[] { string.Empty };
            }

            return input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        private string[] PadStrings(string[] strings, int minWidth) => strings
            .Select(x => x.PadRight(minWidth))
            .ToArray();
    }
}
