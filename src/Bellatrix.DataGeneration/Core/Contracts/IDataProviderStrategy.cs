using Bellatrix.DataGeneration.Core.Parameters;

namespace Bellatrix.DataGeneration;
public interface IDataProviderStrategy
{
    List<TestValue> GenerateTestValues(bool? includeBoundaryValues = null, bool? allowValidEquivalenceClasses = null, bool? allowInvalidEquivalenceClasses = null, params Tuple<string, TestValueCategory>[] customValues);
}