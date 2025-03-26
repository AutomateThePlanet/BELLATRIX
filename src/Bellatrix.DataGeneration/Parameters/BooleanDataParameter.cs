using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class BooleanDataParameter : DataParameter<BooleanDataProviderStrategy>
{
    public BooleanDataParameter(
        bool isManualMode = false,
        int? minBoundary = null,
        int? maxBoundary = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new BooleanDataProviderStrategy(),
              isManualMode,
              false,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}