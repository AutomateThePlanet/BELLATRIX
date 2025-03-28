using Bellatrix.DataGeneration.Configuration;
using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration.TestValueProviders.Base;
public abstract class EquivalenceOnlyDataProviderStrategy : IDataProviderStrategy
{
    protected readonly TestValueGenerationSettings Config;

    protected EquivalenceOnlyDataProviderStrategy()
    {
        Config = ConfigurationService.GetSection<TestValueGenerationSettings>();
    }

    public virtual List<TestValue> GenerateTestValues(
        bool? includeBoundaryValues = null, // Ignored
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] preciseTestValues)
    {
        var testValues = new List<TestValue>();
        var allowValidEquiv = allowValidEquivalenceClasses ?? Config.AllowValidEquivalenceClasses;
        var allowInvalidEquiv = allowInvalidEquivalenceClasses ?? Config.AllowInvalidEquivalenceClasses;

        if (allowValidEquiv)
        {
            var source = Config.InputTypeSettings[GetInputTypeName()].ValidEquivalenceClasses.Select(x => (object)x);

            foreach (var value in source)
            {
                testValues.Add(new TestValue(value, TestValueCategory.Valid));
            }
        }

        if (allowInvalidEquiv)
        {
            var source = Config.InputTypeSettings[GetInputTypeName()].InvalidEquivalenceClasses.Select(x => (object)x);

            foreach (var value in source)
            {
                testValues.Add(new TestValue(value, TestValueCategory.Invalid));
            }
        }

        foreach (var customValue in preciseTestValues)
        {
            testValues.Add(new TestValue(customValue.Value, customValue.Category, customValue.ExpectedInvalidMessage));
        }

        return testValues;
    }

    protected abstract Type GetExpectedType();
    protected abstract string GetInputTypeName();
}
