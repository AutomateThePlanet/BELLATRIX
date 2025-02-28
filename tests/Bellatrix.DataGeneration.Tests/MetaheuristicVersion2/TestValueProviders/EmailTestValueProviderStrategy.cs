using Bellatrix.Web.Contracts;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using System;
using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders
{
    public class EmailTestValueProviderStrategy : BaseTestValueProviderStrategy<Email>
    {
        protected override void AddBoundaryValues(Component component, List<TestValue> testValues)
        {
            int minLength = (component as IComponentMinLength)?.MinLength ?? 5;
            int maxLength = (component as IComponentMaxLength)?.MaxLength ?? 20;

            testValues.Add(new TestValue(GenerateEmail(minLength - 1), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GenerateEmail(minLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateEmail(maxLength), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateEmail(maxLength + 1), TestValueCategory.BoundaryInvalid));
        }

        protected override string GetInputTypeName() => "Email";

        private string GenerateEmail(int length)
        {
            return length < 6 ? "x@x.x" : new string('a', length - 6) + "@mail.com";
        }
    }
}