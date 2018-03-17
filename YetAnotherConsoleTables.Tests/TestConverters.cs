using System;

namespace YetAnotherConsoleTables.Tests
{
    class BaseIntConverter : TableMemberConverter
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

    class BaseStringConverter : TableMemberConverter<string>
    {
        public override string Convert(string value)
        {
            return $"MyConverter:{value}";
        }
    }

    class NoPublicCtor : BaseStringConverter
    {
        private NoPublicCtor()
        {

        }
    }

    class NoParamlessCtor : BaseStringConverter
    {
        public NoParamlessCtor(int a)
        {

        }
    }

    class StaticParamless : BaseStringConverter
    {
        private StaticParamless()
        {

        }

        static StaticParamless()
        {

        }
    }
}
