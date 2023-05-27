namespace YetAnotherConsoleTables.Tests.TestConverters
{
    internal class StringConverter : TableMemberConverter<string>
    {
        public override string Convert(string value)
        {
            return $"MyConverter:{value}";
        }
    }
}
