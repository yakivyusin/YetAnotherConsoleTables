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
        private List<string[]> rowLines = new List<string[]>();

        internal int Length { get; private set; }
        internal IReadOnlyList<string[]> RowLines => rowLines.AsReadOnly();

        internal TableRow(string[] row)
        {
            SplitRow(row);
        }

        private void SplitRow(string[] row)
        {
            if (row == null)
            {
                return;
            }

            var splittedLines = row.Select(column => SplitString(column)).ToList();
            var linesCount = splittedLines.Max(column => column.Length);

            for (int i = 0; i < linesCount; i++)
            {
                rowLines.Add(splittedLines.Select(x => i >= x.Length ? "" : x[i]).ToArray());
            }

            Length = row.Length;
        }

        private string[] SplitString(string input)
        {
            if (input == null)
            {
                return new[] { "" };
            }

            return input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
