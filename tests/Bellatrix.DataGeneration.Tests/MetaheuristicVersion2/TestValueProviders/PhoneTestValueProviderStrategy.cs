using Bellatrix.Web.Contracts;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using System;
using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders
{
    public class PhoneTestValueProviderStrategy : BaseTestValueProviderStrategy<Phone>
    {
        protected override void AddBoundaryValues(Component component, List<TestValue> testValues)
        {
            int minLength = (component as IComponentMinLength)?.MinLength ?? 8;
            int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 15;

            testValues.Add(new TestValue(GeneratePhone(minLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GeneratePhone(maxLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GeneratePhone(minLength - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GeneratePhone(maxLength + 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue("12345", TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Phone";

        private string GeneratePhone(int length)
        {
            return "+359" + new string('9', length - 4);
        }
    }
}