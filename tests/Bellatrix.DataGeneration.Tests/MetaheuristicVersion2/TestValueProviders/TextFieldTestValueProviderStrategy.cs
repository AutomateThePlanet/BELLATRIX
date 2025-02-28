using Bellatrix.Web.Contracts;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using System;
using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders
{
    public class TextFieldTestValueProviderStrategy : BaseTestValueProviderStrategy<TextField>
    {
        protected override void AddBoundaryValues(Component component, List<TestValue> testValues)
        {
            int minLength = (component as IComponentMinLength)?.MinLength ?? 3;
            int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 50;

            testValues.Add(new TestValue(GenerateText(minLength - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GenerateText(minLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateText(maxLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateText(maxLength + 1), TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Text";

        private string GenerateText(int length)
        {
            return new string('A', Math.Max(1, length));
        }
    }
}