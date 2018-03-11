using System;
using System.Reflection;

namespace YetAnotherConsoleTables.Extensions
{
    internal static class MemberInfoExtensions
    {
        public static void SetValue(this MemberInfo member, object property, object value)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                ((PropertyInfo)member).SetValue(property, value, null);
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                ((FieldInfo)member).SetValue(property, value);
            }
            else
            {
                throw new Exception("Property must be of type FieldInfo or PropertyInfo");
            }
        }

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
