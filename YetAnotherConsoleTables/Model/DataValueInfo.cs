using System;
using System.Linq;
using System.Reflection;
using YetAnotherConsoleTables.Attributes;

namespace YetAnotherConsoleTables.Model
{
    internal class DataValueInfo
    {
        private readonly FieldInfo _field;
        private readonly PropertyInfo _property;

        private TableIgnoreAttribute _ignoreAttr;
        private TableMemberAttribute _memberAttr;
        private TableMemberConverter _converter;

        internal DataValueInfo(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Field)
            {
                _field = member as FieldInfo;
            }
            else if (member.MemberType == MemberTypes.Property)
            {
                _property = member as PropertyInfo;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Member must be of type FieldInfo or PropertyInfo");
            }

            ReadAttributes(member);
        }

        internal bool IsIgnored => _ignoreAttr != null;

        internal bool CanRead => _field != null || _property.CanRead;

        internal int Order => _memberAttr?.Order ?? default;

        internal bool IsOrderSpecified => _memberAttr?.IsOrderSpecified ?? false;

        internal string Name => _memberAttr?.DisplayName ?? _field?.Name ?? _property.Name;

        internal int MinWidth => _memberAttr?.MinWidth ?? default;

        internal string GetValue(object obj)
        {
            var value = _field != null ? _field.GetValue(obj) : _property.GetValue(obj);

            return (_converter != null ? _converter.Convert(value) : value?.ToString()) ?? _memberAttr?.DefaultValue;
        }

        private void ReadAttributes(MemberInfo member)
        {
            _ignoreAttr = (TableIgnoreAttribute)Attribute.GetCustomAttribute(member, typeof(TableIgnoreAttribute));
            _memberAttr = (TableMemberAttribute)Attribute.GetCustomAttribute(member, typeof(TableMemberAttribute));
            
            InstantiateConverter((TableMemberConverterAttribute)Attribute.GetCustomAttribute(member, typeof(TableMemberConverterAttribute)));
        }

        private void InstantiateConverter(TableMemberConverterAttribute attr)
        {
            if (attr == null ||
                attr.ConverterType == null ||
                !typeof(TableMemberConverter).IsAssignableFrom(attr.ConverterType))
            {
                return;
            }

            var ctor = attr.ConverterType.GetConstructor(attr.ConstructorParams.Select(x => x.GetType()).ToArray());
            if (ctor != null)
            {
                var converter = (TableMemberConverter)ctor.Invoke(attr.ConstructorParams);
                if (converter.CanConvert(_field != null ? _field.FieldType : _property.PropertyType))
                {
                    _converter = converter;
                }
            }
        }
    }
}
