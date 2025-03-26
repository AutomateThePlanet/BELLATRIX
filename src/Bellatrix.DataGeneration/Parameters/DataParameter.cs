using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration.Parameters;

public class DataParameter<TDataStrategy> : IInputParameter
    where TDataStrategy : class, IDataProviderStrategy
{
    public DataParameter(
        TDataStrategy dataProviderStrategy = null,
        bool isManualMode = false,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
    {
        DataProviderStrategy = dataProviderStrategy;

        try
        {
            TestValues = DataProviderStrategy.GenerateTestValues(
          includeBoundaryValues: isManualMode ? false : includeBoundaryValues, // Disable boundary calculations in manual mode
          allowValidEquivalenceClasses: isManualMode ? false : allowValidEquivalenceClasses,
          allowInvalidEquivalenceClasses: isManualMode ? false : allowInvalidEquivalenceClasses,
          customValues);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
      
    }

    protected TDataStrategy DataProviderStrategy { get; }
    public List<TestValue> TestValues { get; }
}
