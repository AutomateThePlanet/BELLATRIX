using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class MonthDataParameter : DataParameter<MonthDataProviderStrategy>
{
    public MonthDataParameter(
        bool isManualMode = false,
        DateTime? minBoundary = null,
        DateTime? maxBoundary = null,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new MonthDataProviderStrategy(minBoundary, maxBoundary),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}