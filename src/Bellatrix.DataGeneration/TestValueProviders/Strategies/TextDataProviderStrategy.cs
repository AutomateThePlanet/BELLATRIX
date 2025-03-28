using Bellatrix.DataGeneration.TestValueProviders.Base;

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
        var rawText = Faker.Lorem.Sentence(); // e.g., "Lorem ipsum dolor sit amet."
        var finalText = rawText
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput, paddingChar: 'A'); // preserves textual feel

        return new TestValue(finalText, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var parsedSuccessfully = int.TryParse(PrecisionStep, out var step);
        var offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}