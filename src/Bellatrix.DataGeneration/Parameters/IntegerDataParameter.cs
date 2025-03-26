using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class IntegerDataParameter : DataParameter<IntegerDataProviderStrategy>
{
    public IntegerDataParameter(
        bool isManualMode = false,
        int? minBoundary = null,
        int? maxBoundary = null,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new IntegerDataProviderStrategy(minBoundary, maxBoundary),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}