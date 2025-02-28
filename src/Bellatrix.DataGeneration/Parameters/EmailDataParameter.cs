using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters
{
    public class EmailDataParameter : DataParameter<EmailDataProviderStrategy>
    {
        public EmailDataParameter(
            bool isManualMode = false,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params TestValue[] customValues)
            : base(new EmailDataProviderStrategy(),
                  isManualMode,
                  includeBoundaryValues,
                  allowValidEquivalenceClasses,
                  allowInvalidEquivalenceClasses,
                  customValues)
        {
        }
    }
}
