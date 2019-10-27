using System.IO;
using System.Linq;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Represents ConsoleTable output format.
    /// </summary>
    public class ConsoleTableFormat
    {
        private readonly char columnDelimiter;
        private readonly char rowDelimiter;
        private readonly char headerDelimiter;
        private readonly char intersection;
        private readonly bool outsideBorders;

        public static ConsoleTableFormat Default = new ConsoleTableFormat();
        public static ConsoleTableFormat Plus = new ConsoleTableFormat(intersection: '+');
        public static ConsoleTableFormat Header = new ConsoleTableFormat(headerDelimiter: '=', intersection: '|');

        public ConsoleTableFormat(char columnDelimiter = '|', char rowDelimiter = '-',
            char headerDelimiter = '-', char intersection = '-', bool outsideBorders = true)
        {
            this.columnDelimiter = columnDelimiter;
            this.rowDelimiter = rowDelimiter;
            this.headerDelimiter = headerDelimiter;
            this.intersection = intersection;
            this.outsideBorders = outsideBorders;
        }

        internal void Write(ConsoleTable table, TextWriter writer)
        {
            WriteTableHeader(table, writer);
            WriteTableContent(table, writer);
        }

        private void WriteTableHeader(ConsoleTable table, TextWriter writer)
        {
            var headerDelimString = GetRowDelimString(table.ColumnLengths, headerDelimiter);

            if (outsideBorders)
            {
                writer.WriteLine(headerDelimString);
            }

            foreach (var headerLine in table.Headers.RowLines)
            {
                writer.WriteLine(GetRowContent(headerLine, table.ColumnLengths));
            }

            writer.WriteLine(headerDelimString);
        }

        private void WriteTableContent(ConsoleTable table, TextWriter writer)
        {
            var rowDelimString = GetRowDelimString(table.ColumnLengths, rowDelimiter);
            var lastRow = table.Rows.LastOrDefault();

            foreach (var row in table.Rows)
            {
                foreach (var rowLine in row.RowLines)
                {
                    writer.WriteLine(GetRowContent(rowLine, table.ColumnLengths));
                }

                if (outsideBorders || !ReferenceEquals(row, lastRow))
                {
                    writer.WriteLine(rowDelimString);
                }
            }
        }

        private string GetRowDelimString(int[] lengths, char symbol)
        {
            var joined = string.Join(intersection.ToString(),
                lengths.Select(x => new string(symbol, x + 2)));

            return outsideBorders ? $"{intersection}{joined}{intersection}" : joined;
        }

        private string GetRowContent(string[] content, int[] lengths)
        {
            var joined = string.Join(columnDelimiter.ToString(),
                content.Select((x, index) => $" {x}".PadRight(lengths[index] + 2)));

            return outsideBorders ? $"{columnDelimiter}{joined}{columnDelimiter}" : joined;
        }
    }
}
