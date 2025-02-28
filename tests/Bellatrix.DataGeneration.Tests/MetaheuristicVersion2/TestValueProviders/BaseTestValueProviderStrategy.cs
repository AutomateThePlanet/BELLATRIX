using System;
using System.Collections.Generic;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Configuration;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders
{
    public abstract class BaseTestValueProviderStrategy<T> : IComponentTestValuesProviderStrategy<T> where T : Component
    {
        protected readonly TestValueGenerationSettings Config;

        protected BaseTestValueProviderStrategy()
        {
            Config = ConfigurationService.GetSection<TestValueGenerationSettings>();
        }

        public virtual List<TestValue> GenerateTestValues(Component component, bool? includeBoundaryValues = null, bool? allowValidEquivalenceClasses = null, bool? allowInvalidEquivalenceClasses = null, params Tuple<string, TestValueCategory>[] customValues)
        {
            var testValues = new List<TestValue>();

            // Use the provided parameters or fallback to the configuration settings
            var includeBoundary = includeBoundaryValues ?? Config.IncludeBoundaryValues;
            var allowValidEquiv = allowValidEquivalenceClasses ?? Config.AllowValidEquivalenceClasses;
            var allowInvalidEquiv = allowInvalidEquivalenceClasses ?? Config.AllowInvalidEquivalenceClasses;

            // **Boundary Values Handling**
            if (includeBoundary)
            {
                AddBoundaryValues(component, testValues);
            }

            // **Valid Equivalence Classes**
            if (allowValidEquiv)
            {
                foreach (var value in Config.InputTypeSettings[GetInputTypeName()].ValidEquivalenceClasses)
                {
                    testValues.Add(new TestValue(value, TestValueCategory.Valid));
                }
            }

            // **Invalid Equivalence Classes**
            if (allowInvalidEquiv)
            {
                foreach (var value in Config.InputTypeSettings[GetInputTypeName()].InvalidEquivalenceClasses)
                {
                    testValues.Add(new TestValue(value, TestValueCategory.Invalid));
                }
            }

            // **Custom Values**
            foreach (var customValue in customValues)
            {
                testValues.Add(new TestValue(customValue.Item1, customValue.Item2));
            }

            return testValues;
        }

        // Template Method Design Pattern
        protected abstract void AddBoundaryValues(Component component, List<TestValue> testValues);
        protected abstract string GetInputTypeName();
    }
}
