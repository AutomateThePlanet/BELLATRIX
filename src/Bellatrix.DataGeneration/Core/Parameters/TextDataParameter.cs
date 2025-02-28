using Bellatrix.DataGeneration.Core.Contracts;
using Bellatrix.DataGeneration.Core.Parameters;
using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Core
{
    public class TextDataParameter : DataParameter<TextDataProviderStrategy>
    {
        public TextDataParameter(
            bool isManualMode = false,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params Tuple<string, TestValueCategory>[] customValues)
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
