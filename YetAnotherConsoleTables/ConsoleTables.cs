using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YetAnotherConsoleTables.Attributes;
using YetAnotherConsoleTables.Extensions;
using YetAnotherConsoleTables.Model;

namespace YetAnotherConsoleTables
{
    public class ConsoleTables
    {
        internal int[] ColumnLengths { get; private set; }
        internal TableRow Headers { get; private set; }

        private ConsoleTables(TableRow headers)
        {
            Headers = headers;
            ColumnLengths = new int[headers.Length];
            CheckColumnLengths(headers);
        }

        public static ConsoleTables From<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            var type = typeof(T);
            var members = GetTypeMembers(type);

            if (members.Length == 0)
            {
                throw new InvalidOperationException("Class doesn't contains info.");
            }

            var table = new ConsoleTables(
                new TableRow(members.Select(x => GetMemberName(x)).ToArray()));

            return table;
        }

        private static MemberInfo[] GetTypeMembers(Type type)
        {
            var ignoreAttr = typeof(TableIgnoreAttribute);
            return type.GetMembers()
                .Where(m => (m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field) &&
                    m.CanRead() &&
                    !Attribute.IsDefined(m, ignoreAttr))
                .ToArray();
        }

        private static string GetMemberName(MemberInfo member)
        {
            var displayNameAttr = (TableDisplayNameAttribute)
                Attribute.GetCustomAttribute(member, typeof(TableDisplayNameAttribute));

            return displayNameAttr == null ? member.Name : displayNameAttr.Name;
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
