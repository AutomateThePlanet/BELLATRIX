using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration.Parameters;

public class DataParameter<TDataStrategy> : IInputParameter
    where TDataStrategy : class, IDataProviderStrategy
{
    public DataParameter(
        TDataStrategy dataProviderStrategy = null,
        bool preciseMode = false,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] preciseTestValues)
    {
        DataProviderStrategy = dataProviderStrategy;

        TestValues = DataProviderStrategy.GenerateTestValues(
         includeBoundaryValues: preciseMode ? false : includeBoundaryValues, // Disable boundary calculations in manual mode
         allowValidEquivalenceClasses: preciseMode ? false : allowValidEquivalenceClasses,
         allowInvalidEquivalenceClasses: preciseMode ? false : allowInvalidEquivalenceClasses,
         preciseTestValues);
    }

    protected TDataStrategy DataProviderStrategy { get; }
    public List<TestValue> TestValues { get; }
}
