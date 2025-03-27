using Bellatrix.DataGeneration.TestValueProviders;

namespace Bellatrix.DataGeneration.Parameters;

/// <summary>
/// Represents a parameter for a multi-select input (e.g., checkboxes or multi-dropdown).
/// Accepts a list of string array sets for valid and invalid selections.
/// </summary>
/// <example>
/// var parameter = new MultiSelectDataParameter(
///     validOptions: new List<object> { new[] { "Option1", "Option2" }, new[] { "Option3" } },
///     invalidOptions: new List<object> { new[] { "None" }, new string[0] });
/// </example>
public class MultiSelectDataParameter : DataParameter<MultiSelectDataProviderStrategy>
{
    public MultiSelectDataParameter(
        List<object> validOptions,
        List<object> invalidOptions,
        bool preciseMode = false,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] preciseTestValues)
        : base(new MultiSelectDataProviderStrategy(validOptions, invalidOptions),
              preciseMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              preciseTestValues)
    {
    }
}
