using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class UrlDataParameter : DataParameter<UrlDataProviderStrategy>
{
    public UrlDataParameter(
        bool isManualMode = false,
        int? minBoundary = null,
        int? maxBoundary = null,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new UrlDataProviderStrategy(minBoundary, maxBoundary),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}