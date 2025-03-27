namespace Bellatrix.DataGeneration.Contracts;
public interface IDataProviderStrategy
{
    List<TestValue> GenerateTestValues(bool? includeBoundaryValues = null, bool? allowValidEquivalenceClasses = null, bool? allowInvalidEquivalenceClasses = null, params TestValue[] preciseTestValues);
}