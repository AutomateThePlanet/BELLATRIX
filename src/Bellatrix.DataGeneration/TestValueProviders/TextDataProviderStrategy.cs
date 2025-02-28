using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders
{
    public class TextDataProviderStrategy : DataProviderStrategy
    {
        protected override void AddBoundaryValues(List<TestValue> testValues)
        {
            //int minLength = (component as IComponentMinLength)?.MinLength ?? 3;
            //int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 50;

            // TODO: provide from constructor
            testValues.Add(new TestValue(GenerateText(3 - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GenerateText(3), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateText(50), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateText(50 + 1), TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Text";

        private string GenerateText(int length)
        {
            return new string('A', Math.Max(1, length));
        }
    }
}