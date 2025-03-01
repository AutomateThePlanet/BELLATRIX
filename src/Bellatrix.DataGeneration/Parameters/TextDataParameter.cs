using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters
{
    public class TextDataParameter : DataParameter<TextDataProviderStrategy>
    {
        public TextDataParameter(
            bool isManualMode = false,
            int? minBoundary = null,
            int? maxBoundary = null,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params TestValue[] customValues)
            : base(new TextDataProviderStrategy(minBoundary, maxBoundary),
                  isManualMode,
                  includeBoundaryValues,
                  allowValidEquivalenceClasses,
                  allowInvalidEquivalenceClasses,
                  customValues)
        {
        }
    }
}
