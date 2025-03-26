using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class TextDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public TextDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Text";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        string text = GenerateText(boundaryInput);
        return new TestValue(text, typeof(string), category);
    }

    private string GenerateText(int length)
    {
        return new string('A', Math.Max(1, length));
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsedSuccessfully = int.TryParse(PrecisionStep, out int step);
        int offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}