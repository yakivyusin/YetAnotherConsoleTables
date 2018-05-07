using System;
using System.Reflection;
using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Model
{
    internal class DataValueInfo
    {
        private FieldInfo field;
        private PropertyInfo property;

        private TableIgnoreAttribute ignoreAttr;
        private TableMemberAttribute memberAttr;
        private TableMemberConverter converter;

        internal DataValueInfo(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Field)
            {
                field = member as FieldInfo;
                ReadAttributes(member);
            }
            else if (member.MemberType == MemberTypes.Property)
            {
                property = member as PropertyInfo;
                ReadAttributes(member);
            }
            else
            {
                throw new Exception("Member must be of type FieldInfo or PropertyInfo");
            }
        }

        internal bool IsIgnored => ignoreAttr != null;

        internal bool CanRead => field != null ? true : property.CanRead;

        internal int? Order => memberAttr?.NullableOrder;

        internal string Name
        {
            get
            {
                if (memberAttr?.DisplayName != null)
                {
                    return memberAttr.DisplayName;
                }
                else if (field != null)
                {
                    return field.Name;
                }
                else
                {
                    return property.Name;
                }
            }
        }

        internal string GetValue(object obj)
        {
            var value = field != null ? field.GetValue(obj) : property.GetValue(obj);

            if (converter == null)
            {
                return value?.ToString() ?? memberAttr?.DefaultValue;
            }
            else
            {
                return converter.Convert(value) ?? memberAttr?.DefaultValue;
            }
        }

        private void ReadAttributes(MemberInfo member)
        {
            ignoreAttr = (TableIgnoreAttribute)Attribute
                .GetCustomAttribute(member, typeof(TableIgnoreAttribute));
            memberAttr = (TableMemberAttribute)Attribute
                .GetCustomAttribute(member, typeof(TableMemberAttribute));
            InstantiateConverter((TableMemberConverterAttribute)Attribute
                .GetCustomAttribute(member, typeof(TableMemberConverterAttribute)));
        }

        private void InstantiateConverter(TableMemberConverterAttribute attr)
        {
            if (attr == null || attr.ConverterType == null ||
                !typeof(TableMemberConverter).IsAssignableFrom(attr.ConverterType))
            {
                return;
            }

            var ctor = attr.ConverterType.GetConstructor(Type.EmptyTypes);
            if (ctor != null)
            {
                var temp = (TableMemberConverter)ctor.Invoke(new object[] { });
                if (temp.CanConvert(field != null ? field.FieldType : property.PropertyType))
                {
                    converter = temp;
                }
            }
        }
    }
}
