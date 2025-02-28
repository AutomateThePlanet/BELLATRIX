using Bellatrix.DataGeneration.Core.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders
{
    public class PhoneDataProviderStrategy : DataProviderStrategy
    {
        protected override void AddBoundaryValues(List<TestValue> testValues)
        {
            //int minLength = (component as IComponentMinLength)?.MinLength ?? 8;
            //int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 15;

            testValues.Add(new TestValue(GeneratePhone(8), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GeneratePhone(15), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GeneratePhone(8 - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GeneratePhone(15 + 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue("12345", TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Phone";

        private string GeneratePhone(int length)
        {
            return "+359" + new string('9', length - 4);
        }
    }
}