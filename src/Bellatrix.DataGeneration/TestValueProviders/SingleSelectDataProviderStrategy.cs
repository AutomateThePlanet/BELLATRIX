namespace Bellatrix.DataGeneration.TestValueProviders;

public class SingleSelectDataProviderStrategy : EquivalenceOnlyDataProviderStrategy
{
    protected override string GetInputTypeName() => "SingleSelect";

    protected override Type GetExpectedType() => typeof(string);
}
