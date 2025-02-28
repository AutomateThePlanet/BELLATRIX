using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders
{
    public class EmailDataProviderStrategy : DataProviderStrategy
    {
        protected override void AddBoundaryValues(List<TestValue> testValues)
        {
            //int minLength = (component as IComponentMinLength)?.MinLength ?? 5;
            //int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 20;

            testValues.Add(new TestValue(GenerateEmail(5 - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GenerateEmail(5), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateEmail(20), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateEmail(20 + 1), TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Email";

        private string GenerateEmail(int length)
        {
            return length < 6 ? "x@x.x" : new string('a', length - 6) + "@mail.com";
        }
    }
}