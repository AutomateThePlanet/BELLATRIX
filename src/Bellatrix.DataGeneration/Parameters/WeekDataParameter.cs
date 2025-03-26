using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class WeekDataParameter : DataParameter<WeekDataProviderStrategy>
{
    public WeekDataParameter(
        bool isManualMode = false,
        DateTime? minBoundary = null,
        DateTime? maxBoundary = null,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new WeekDataProviderStrategy(minBoundary, maxBoundary),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}