using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters
{
    public class EmailDataParameter : DataParameter<EmailDataProviderStrategy>
    {
        public EmailDataParameter(
            bool isManualMode = false,
            int? minBoundary = null, 
            int? maxBoundary = null,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params TestValue[] customValues)
            : base(new EmailDataProviderStrategy(minBoundary, maxBoundary),
                  isManualMode,
                  includeBoundaryValues,
                  allowValidEquivalenceClasses,
                  allowInvalidEquivalenceClasses,
                  customValues)
        {
        }
    }
}
