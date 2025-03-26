using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class MultiSelectDataProviderStrategy : EquivalenceOnlyDataProviderStrategy
{
    public MultiSelectDataProviderStrategy(
        List<object> validOptions,
        List<object> invalidOptions)
        : base(validOptions, invalidOptions)
    {
    }

    protected override string GetInputTypeName() => "MultiSelect";

    protected override Type GetExpectedType() => typeof(string[]);
}
