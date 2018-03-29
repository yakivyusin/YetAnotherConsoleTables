using System;
using System.Linq;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Represents ConsoleTable output format.
    /// </summary>
    public class ConsoleTableFormat
    {
        private char columnDelimiter;
        private char rowDelimiter;
        private char headerDelimiter;
        private char intersection;

        public static ConsoleTableFormat Default = new ConsoleTableFormat();
        public static ConsoleTableFormat Plus = new ConsoleTableFormat(intersection: '+');
        public static ConsoleTableFormat Header = new ConsoleTableFormat(headerDelimiter: '=', intersection: '|');

        public ConsoleTableFormat(char columnDelimiter = '|', char rowDelimiter = '-',
            char headerDelimiter = '-', char intersection = '-')
        {
            this.columnDelimiter = columnDelimiter;
            this.rowDelimiter = rowDelimiter;
            this.headerDelimiter = headerDelimiter;
            this.intersection = intersection;
        }

        internal void Write(ConsoleTable table)
        {
            var header = table.Headers;

            var rowDelimString = GetRowDelimString(table.ColumnLengths, rowDelimiter);
            var headerDelimString = rowDelimiter == headerDelimiter ?
                rowDelimString : GetRowDelimString(table.ColumnLengths, headerDelimiter);

            Console.WriteLine(headerDelimString);
            foreach (var headerLine in header.RowLines)
            {
                Console.WriteLine(GetRowContent(headerLine, table.ColumnLengths));
            }
            Console.WriteLine(headerDelimString);

            foreach (var row in table.Rows)
            {
                foreach (var rowLine in row.RowLines)
                {
                    Console.WriteLine(GetRowContent(rowLine, table.ColumnLengths));
                }
                Console.WriteLine(rowDelimString);
            }
        }

        private string GetRowDelimString(int[] lengths, char symbol)
        {
            var joined = string.Join(intersection.ToString(),
                lengths.Select(x => new string(symbol, x + 2)));

            return $"{intersection}{joined}{intersection}";
        }

        private string GetRowContent(string[] content, int[] lengths)
        {
            var joined = string.Join(columnDelimiter.ToString(),
                content.Select((x, index) => $" {x}{new string(' ', lengths[index] - x.Length + 1)}"));

            return $"{columnDelimiter}{joined}{columnDelimiter}";
        }
    }
}
