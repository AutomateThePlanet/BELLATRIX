using Bellatrix.DataGeneration.TestValueProviders.Base;

namespace Bellatrix.DataGeneration.TestValueProviders;
public class MultiSelectDataProviderStrategy : EquivalenceOnlyDataProviderStrategy
{
    protected override string GetInputTypeName() => "MultiSelect";

    protected override Type GetExpectedType() => typeof(string[]);
}
