using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters
{
    public class TextDataParameter : DataParameter<TextDataProviderStrategy>
    {
        public TextDataParameter(
            bool isManualMode = false,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params TestValue[] customValues)
            : base(new TextDataProviderStrategy(),
                  isManualMode,
                  includeBoundaryValues,
                  allowValidEquivalenceClasses,
                  allowInvalidEquivalenceClasses,
                  customValues)
        {
        }
    }
}
