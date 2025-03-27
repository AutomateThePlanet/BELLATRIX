using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

/// <summary>
/// Represents a parameter for a single select/dropdown input.
/// Accepts a predefined list of valid and invalid options.
/// </summary>
/// <example>
/// var parameter = new SingleSelectDataParameter(
///     validOptions: new List<object> { "Option1", "Option2" },
///     invalidOptions: new List<object> { "InvalidOption", "", null });
/// </example>
public class SingleSelectDataParameter : DataParameter<SingleSelectDataProviderStrategy>
{
    public SingleSelectDataParameter(
        bool preciseMode = false,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] preciseTestValues)
        : base(new SingleSelectDataProviderStrategy(),
              preciseMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              preciseTestValues)
    {
    }
}