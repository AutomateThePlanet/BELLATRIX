using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class UsernameDataParameter : DataParameter<UsernameDataProviderStrategy>
{
    public UsernameDataParameter(
        bool isManualMode = false,
        int? minBoundary = null,
        int? maxBoundary = null,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new UsernameDataProviderStrategy(minBoundary, maxBoundary),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}