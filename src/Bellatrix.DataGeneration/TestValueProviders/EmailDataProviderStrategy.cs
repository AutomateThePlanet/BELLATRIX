using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders
{
    public class EmailDataProviderStrategy : DataProviderStrategy
    {
        public EmailDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null) 
            : base(minBoundary, maxBoundary)
        {
        }

        protected override string GetInputTypeName() => "Email";

        protected override string GenerateValue(int length)
        {
            return length < 6 ? "x@x.x" : new string('a', length - 6) + "@mail.com";
        }
    }
}