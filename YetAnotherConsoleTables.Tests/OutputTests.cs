using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using YetAnotherConsoleTables.Tests.TestClasses;

namespace YetAnotherConsoleTables.Tests
{
    public class OutputTests
    {
        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void FormatOutputTest(bool inConsole, ConsoleTableFormat format, string[] resultRows)
        {
            var table = ConsoleTable.From(new[]
            {
                new Properties
                {
                    Property1 = "AA",
                    Property2 = 3
                },
                new Properties
                {
                    Property1 = "AA",
                    Property2 = 3
                }
            });
            var writer = new Writer();

            if (inConsole)
            {
                var consoleStdOut = Console.Out;

                Console.SetOut(writer);
                table.Write(format);
                Console.SetOut(consoleStdOut);
            }
            else
            {
                table.Write(format, writer);
            }

            Assert.Equal(resultRows, writer.Values);
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            foreach (var inConsole in new[] { true, false })
            {
                yield return new object[]
                {
                    inConsole,
                    ConsoleTableFormat.Default,
                    new[]
                    {
                        "-------------------------",
                        "| Property1 | Property2 |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    ConsoleTableFormat.Plus,
                    new[]
                    {
                        "+-----------+-----------+",
                        "| Property1 | Property2 |",
                        "+-----------+-----------+",
                        "| AA        | 3         |",
                        "+-----------+-----------+",
                        "| AA        | 3         |",
                        "+-----------+-----------+"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    ConsoleTableFormat.Header,
                    new[]
                    {
                        "|===========|===========|",
                        "| Property1 | Property2 |",
                        "|===========|===========|",
                        "| AA        | 3         |",
                        "|-----------|-----------|",
                        "| AA        | 3         |",
                        "|-----------|-----------|"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    ConsoleTableFormat.GithubMarkdown,
                    new[]
                    {
                        "| Property1 | Property2 |",
                        "|-----------|-----------|",
                        "| AA        | 3         |",
                        "| AA        | 3         |"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.Left),
                    new[]
                    {
                        "------------------------",
                        " Property1 | Property2 |",
                        "------------------------",
                        " AA        | 3         |",
                        "------------------------",
                        " AA        | 3         |",
                        "------------------------"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.Right),
                    new[]
                    {
                        "------------------------",
                        "| Property1 | Property2 ",
                        "------------------------",
                        "| AA        | 3         ",
                        "------------------------",
                        "| AA        | 3         ",
                        "------------------------"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.Top),
                    new[]
                    {
                        "| Property1 | Property2 |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.Bottom),
                    new[]
                    {
                        "-------------------------",
                        "| Property1 | Property2 |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------",
                        "| AA        | 3         |"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.HeaderDelimiter),
                    new[]
                    {
                        "-------------------------",
                        "| Property1 | Property2 |",
                        "| AA        | 3         |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "-------------------------"
                    }
                };

                yield return new object[]
                {
                    inConsole,
                    new ConsoleTableFormat(borders: ConsoleTableFormat.Borders.All & ~ConsoleTableFormat.Borders.RowDelimiter),
                    new[]
                    {
                        "-------------------------",
                        "| Property1 | Property2 |",
                        "-------------------------",
                        "| AA        | 3         |",
                        "| AA        | 3         |",
                        "-------------------------",
                    }
                };
            }
        }


        private class Writer : TextWriter
        {
            public List<string> Values = new List<string>();

            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            public override void WriteLine(string value)
            {
                base.WriteLine(value);
                Values.Add(value);
            }
        }
    }
}
