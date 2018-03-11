using System;
using System.Reflection;

namespace YetAnotherConsoleTables.Extensions
{
    internal static class MemberInfoExtensions
    {
        public static object GetValue(this MemberInfo member, object property)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).GetValue(property, null);
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)member).GetValue(property);
            }
            else
            {
                throw new Exception("Property must be of type FieldInfo or PropertyInfo");
            }
        }

        public static bool CanRead(this MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).CanRead;
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                return true;
            }
            else
            {
                throw new Exception("Property must be of type FieldInfo or PropertyInfo");
            }
        }
    }
}
