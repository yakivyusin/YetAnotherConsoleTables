﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YetAnotherConsoleTables.Attributes;
using YetAnotherConsoleTables.Model;

namespace YetAnotherConsoleTables
{
    public class ConsoleTable
    {
        private List<TableRow> rows = new List<TableRow>();

        internal int[] ColumnLengths { get; private set; }
        internal TableRow Headers { get; private set; }
        internal IReadOnlyList<TableRow> Rows => rows.AsReadOnly();

        private ConsoleTable(TableRow headers)
        {
            Headers = headers;
            ColumnLengths = new int[headers.Length];
            CheckColumnLengths(headers);
        }

        /// <summary>
        /// Creates ConsoleTable object from passed <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Data to output.</param>
        /// <returns>Created ConsoleTable object.</returns>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="collection"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Throws when <typeparamref name="T"/> doesn't contain data to output.</exception>
        public static ConsoleTable From<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            var type = GetElementType(collection.GetType());
            var members = GetTypeMembers(type);

            if (members.Length == 0)
            {
                throw new InvalidOperationException("Class doesn't contain info.");
            }

            var table = new ConsoleTable(
                new TableRow(members.Select(x => x.Name).ToArray()));
            foreach (var item in collection)
            {
                if (item != null)
                {
                    var rowItems = members.Select(x => x.GetValue(item)).ToArray();
                    table.AddRow(new TableRow(rowItems));
                }
            }

            return table;
        }

        private static Type GetElementType(Type collectionType)
        {
            if (collectionType.IsArray)
            {
                return collectionType.GetElementType();
            }

            // type is IEnumerable<T>;
            if (collectionType.IsGenericType &&
                collectionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return collectionType.GetGenericArguments()[0];
            }

            // type implements/extends IEnumerable<T>;
            var enumType = collectionType.GetInterfaces()
                                    .Where(t => t.IsGenericType &&
                                           t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();

            return enumType ?? collectionType;
        }

        private static DataValueInfo[] GetTypeMembers(Type type)
        {
            var ignoreAttr = typeof(TableIgnoreAttribute);
            return type.GetMembers()
                .Where(m => m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field)
                .Select(m => new DataValueInfo(m))
                .Where(m => !m.IsIgnored && m.CanRead)
                .OrderByDescending(m => m.Order.HasValue)
                .ThenBy(m => m.Order)
                .ToArray();
        }

        /// <summary>
        /// Writes ConsoleTable to Console using Default format.
        /// </summary>
        public void Write()
        {
            Write(ConsoleTableFormat.Default, Console.Out);
        }

        /// <summary>
        /// Writes ConsoleTable to passed <paramref name="destination"/> using Default format.
        /// </summary>
        /// <param name="destination">Destination.</param>
        public void Write(TextWriter destination)
        {
            Write(ConsoleTableFormat.Default, destination);
        }

        /// <summary>
        /// Writes ConsoleTable to Console using passed <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Output format.</param>
        public void Write(ConsoleTableFormat format)
        {
            Write(format, Console.Out);
        }

        /// <summary>
        /// Writes ConsoleTable to passed <paramref name="destination"/> using passed <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Output format.</param>
        /// <param name="destination">Destination.</param>
        public void Write(ConsoleTableFormat format, TextWriter destination)
        {
            format.Write(this, destination);
        }

        private void AddRow(TableRow row)
        {
            rows.Add(row);
            CheckColumnLengths(row);
        }

        private void CheckColumnLengths(TableRow row)
        {
            foreach (var line in row.RowLines)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (line[i].Length > ColumnLengths[i])
                    {
                        ColumnLengths[i] = line[i].Length;
                    }
                }
            }
        }
    }
}
