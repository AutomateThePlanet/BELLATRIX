using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

public class ColorDataParameter : DataParameter<ColorDataProviderStrategy>
{
    public ColorDataParameter(
         List<object> validOptions = null,
        List<object> invalidOptions = null,
        bool isManualMode = false,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new ColorDataProviderStrategy(),
              isManualMode,
              false,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}
