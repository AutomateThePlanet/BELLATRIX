using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class SingleSelectDataProviderStrategy : EquivalenceOnlyDataProviderStrategy
{
    public SingleSelectDataProviderStrategy(
        List<object> validOptions,
        List<object> invalidOptions)
        : base(validOptions, invalidOptions)
    {
    }

    protected override string GetInputTypeName() => "SingleSelect";

    protected override Type GetExpectedType() => typeof(string);
}
