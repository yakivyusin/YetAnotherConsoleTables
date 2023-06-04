namespace YetAnotherConsoleTables.Tests.TestConverters
{
    internal class NoParamlessCtorConverter : StringConverter
    {
        private readonly int _param;

        public NoParamlessCtorConverter(int param)
        {
            _param = param;
        }

        public override string Convert(string value)
        {
            return $"{base.Convert(value)}:{_param}";
        }
    }
}
