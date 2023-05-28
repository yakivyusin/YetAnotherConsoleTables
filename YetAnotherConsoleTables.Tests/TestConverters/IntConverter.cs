using System;

namespace YetAnotherConsoleTables.Tests.TestConverters
{
    internal class IntConverter : TableMemberConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }

        public override string Convert(object value)
        {
            var cast = (int)value;

            return cast.ToString("+0;-#");
        }
    }
}
