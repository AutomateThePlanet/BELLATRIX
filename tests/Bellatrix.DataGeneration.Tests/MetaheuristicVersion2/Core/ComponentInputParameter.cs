using System;
using System.Collections.Generic;
using Bellatrix.Web.GettingStarted;
using Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Core
{
    public class ComponentInputParameter<T> : IInputParameter where T : Component
    {
        public T Component { get; }
        public List<TestValue> TestValues { get; }

        public ComponentInputParameter(
            T component = null,
            bool isManualMode = false,
            bool? includeBoundaryValues = null,
            bool? allowValidEquivalenceClasses = null,
            bool? allowInvalidEquivalenceClasses = null,
            params Tuple<string, TestValueCategory>[] customValues)
        {
            Component = component;
            var testValueProviderStrategy = ServicesCollection.Current.Resolve<IComponentTestValuesProviderStrategy<T>>();

            TestValues = testValueProviderStrategy.GenerateTestValues(
                component,
                includeBoundaryValues: isManualMode ? false : includeBoundaryValues, // Disable boundary calculations in manual mode
                allowValidEquivalenceClasses,
                allowInvalidEquivalenceClasses,
                customValues);
        }
    }
}
