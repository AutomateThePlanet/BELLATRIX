namespace Bellatrix.DataGeneration.TestValueProviders
{
    public class TextDataProviderStrategy : DataProviderStrategy
    {
        public TextDataProviderStrategy(int minBoundary, int maxBoundary) 
            : base(minBoundary, maxBoundary)
        {
        }

        protected override string GetInputTypeName() => "Text";

        protected override string GenerateValue(int length)
        {
            return new string('A', Math.Max(1, length));
        }
    }
}