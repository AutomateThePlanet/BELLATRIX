using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class TextDataProviderStrategy : DataProviderStrategy
{
    public TextDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Text";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var text = GenerateText(boundaryInput);
        return new TestValue(text, typeof(string), category);
    }

    private string GenerateText(int length)
    {
        return new string('A', Math.Max(1, length));
    }
}