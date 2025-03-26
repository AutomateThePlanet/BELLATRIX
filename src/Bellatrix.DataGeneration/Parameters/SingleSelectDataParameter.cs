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
        List<object> validOptions,
        List<object> invalidOptions,
        bool isManualMode = false,
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
        : base(new SingleSelectDataProviderStrategy(validOptions, invalidOptions),
              isManualMode,
              includeBoundaryValues,
              allowValidEquivalenceClasses,
              allowInvalidEquivalenceClasses,
              customValues)
    {
    }
}