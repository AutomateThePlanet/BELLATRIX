using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters
{
    public class PhoneDataParameter : DataParameter<PhoneDataProviderStrategy>
    {
        public PhoneDataParameter(
            bool isManualMode = false,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params TestValue[] customValues)
            : base(new PhoneDataProviderStrategy(),
                  isManualMode,
                  includeBoundaryValues,
                  allowValidEquivalenceClasses,
                  allowInvalidEquivalenceClasses,
                  customValues)
        {
        }
    }
}
