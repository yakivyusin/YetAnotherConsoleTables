using System.IO;
using System.Linq;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Represents ConsoleTable output format.
    /// </summary>
    public partial class ConsoleTableFormat
    {
        private readonly char _rowDelimiter;
        private readonly char _headerDelimiter;
        private readonly string _columnDelimiter;
        private readonly string _intersection;
        private readonly Borders _borders;

        public static ConsoleTableFormat Default = new ConsoleTableFormat();
        public static ConsoleTableFormat Plus = new ConsoleTableFormat(intersection: '+');
        public static ConsoleTableFormat Header = new ConsoleTableFormat(headerDelimiter: '=', intersection: '|');
        public static ConsoleTableFormat GithubMarkdown = new ConsoleTableFormat(intersection: '|', borders: Borders.Left | Borders.Right | Borders.HeaderDelimiter);

        public ConsoleTableFormat(
            char columnDelimiter = '|',
            char rowDelimiter = '-',
            char headerDelimiter = '-',
            char intersection = '-',
            Borders borders = Borders.All)
        {
            _columnDelimiter = columnDelimiter.ToString();
            _rowDelimiter = rowDelimiter;
            _headerDelimiter = headerDelimiter;
            _intersection = intersection.ToString();
            _borders = borders;
        }

        internal void Write(ConsoleTable table, TextWriter writer)
        {
            WriteTableHeader(table, writer);
            WriteTableContent(table, writer);
        }

        private void WriteTableHeader(ConsoleTable table, TextWriter writer)
        {
            var headerDelimiterString = GetRowDelimiterString(table.ColumnLengths, _headerDelimiter);

            if (_borders.HasFlag(Borders.Top))
            {
                writer.WriteLine(headerDelimiterString);
            }

            foreach (var headerLine in table.Headers.RowLines)
            {
                writer.WriteLine(GetRowContent(headerLine, table.ColumnLengths));
            }

            if (_borders.HasFlag(Borders.HeaderDelimiter))
            {
                writer.WriteLine(headerDelimiterString);
            }
        }

        private void WriteTableContent(ConsoleTable table, TextWriter writer)
        {
            var rowDelimiterString = GetRowDelimiterString(table.ColumnLengths, _rowDelimiter);
            var hasRowDelimiterString = _borders.HasFlag(Borders.RowDelimiter);
            var lastRow = table.Rows.LastOrDefault();

            foreach (var row in table.Rows)
            {
                foreach (var rowLine in row.RowLines)
                {
                    writer.WriteLine(GetRowContent(rowLine, table.ColumnLengths));
                }

                var isLastRow = ReferenceEquals(row, lastRow);
                if ((_borders.HasFlag(Borders.Bottom) && isLastRow) ||
                    (hasRowDelimiterString && !isLastRow))
                {
                    writer.WriteLine(rowDelimiterString);
                }
            }
        }

        private string GetRowDelimiterString(int[] lengths, char symbol)
        {
            var joined = string.Join(_intersection, lengths.Select(x => new string(symbol, x + 2)));
            var leftBorder = _borders.HasFlag(Borders.Left) ? _intersection : string.Empty;
            var rightBorder = _borders.HasFlag(Borders.Right) ? _intersection : string.Empty;

            return $"{leftBorder}{joined}{rightBorder}";
        }

        private string GetRowContent(string[] content, int[] lengths)
        {
            var joined = string.Join(_columnDelimiter, content.Select((x, index) => $" {x}".PadRight(lengths[index] + 2)));
            var leftBorder = _borders.HasFlag(Borders.Left) ? _columnDelimiter : string.Empty;
            var rightBorder = _borders.HasFlag(Borders.Right) ? _columnDelimiter : string.Empty;

            return $"{leftBorder}{joined}{rightBorder}";
        }
    }
}
