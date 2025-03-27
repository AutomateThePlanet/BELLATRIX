namespace Bellatrix.DataGeneration.TestValueProviders;

public class BooleanDataProviderStrategy : EquivalenceOnlyDataProviderStrategy
{
    protected override string GetInputTypeName() => "Boolean";

    protected override Type GetExpectedType() => typeof(bool);
}