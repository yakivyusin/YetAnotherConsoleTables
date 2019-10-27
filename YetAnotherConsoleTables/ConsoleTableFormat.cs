using System.IO;
using System.Linq;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Represents ConsoleTable output format.
    /// </summary>
    public partial class ConsoleTableFormat
    {
        private readonly char rowDelimiter;
        private readonly char headerDelimiter;
        private readonly string columnDelimiter;
        private readonly string intersection;
        private readonly Borders borders;

        public static ConsoleTableFormat Default = new ConsoleTableFormat();
        public static ConsoleTableFormat Plus = new ConsoleTableFormat(intersection: '+');
        public static ConsoleTableFormat Header = new ConsoleTableFormat(headerDelimiter: '=', intersection: '|');

        public ConsoleTableFormat(char columnDelimiter = '|', char rowDelimiter = '-',
            char headerDelimiter = '-', char intersection = '-', Borders borders = Borders.All)
        {
            this.columnDelimiter = columnDelimiter.ToString();
            this.rowDelimiter = rowDelimiter;
            this.headerDelimiter = headerDelimiter;
            this.intersection = intersection.ToString();
            this.borders = borders;
        }

        internal void Write(ConsoleTable table, TextWriter writer)
        {
            WriteTableHeader(table, writer);
            WriteTableContent(table, writer);
        }

        private void WriteTableHeader(ConsoleTable table, TextWriter writer)
        {
            var headerDelimString = GetRowDelimString(table.ColumnLengths, headerDelimiter);

            if (borders.HasFlag(Borders.Top))
            {
                writer.WriteLine(headerDelimString);
            }

            foreach (var headerLine in table.Headers.RowLines)
            {
                writer.WriteLine(GetRowContent(headerLine, table.ColumnLengths));
            }

            if (borders.HasFlag(Borders.HeaderDelimiter))
            {
                writer.WriteLine(headerDelimString);
            }
        }

        private void WriteTableContent(ConsoleTable table, TextWriter writer)
        {
            var rowDelimString = GetRowDelimString(table.ColumnLengths, rowDelimiter);
            var hasRowDelimString = borders.HasFlag(Borders.RowDelimiter);
            var lastRow = table.Rows.LastOrDefault();

            foreach (var row in table.Rows)
            {
                foreach (var rowLine in row.RowLines)
                {
                    writer.WriteLine(GetRowContent(rowLine, table.ColumnLengths));
                }

                if (borders.HasFlag(Borders.Bottom) ||
                    (!ReferenceEquals(row, lastRow) && hasRowDelimString))
                {
                    writer.WriteLine(rowDelimString);
                }
            }
        }

        private string GetRowDelimString(int[] lengths, char symbol)
        {
            var joined = string.Join(intersection.ToString(),
                lengths.Select(x => new string(symbol, x + 2)));
            var leftBorder = borders.HasFlag(Borders.Left) ? intersection : string.Empty;
            var rightBorder = borders.HasFlag(Borders.Right) ? intersection : string.Empty;

            return $"{leftBorder}{joined}{rightBorder}";
        }

        private string GetRowContent(string[] content, int[] lengths)
        {
            var joined = string.Join(columnDelimiter.ToString(),
                content.Select((x, index) => $" {x}".PadRight(lengths[index] + 2)));
            var leftBorder = borders.HasFlag(Borders.Left) ? columnDelimiter : string.Empty;
            var rightBorder = borders.HasFlag(Borders.Right) ? columnDelimiter : string.Empty;

            return $"{leftBorder}{joined}{rightBorder}";
        }
    }
}
