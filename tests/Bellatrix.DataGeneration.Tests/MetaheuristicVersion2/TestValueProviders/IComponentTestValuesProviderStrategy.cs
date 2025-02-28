using System.Collections.Generic;
using System;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders;
public interface IComponentTestValuesProviderStrategy<TComponent>
   where TComponent : Component
{
    List<TestValue> GenerateTestValues(Component component, bool? includeBoundaryValues = null, bool? allowValidEquivalenceClasses = null, bool? allowInvalidEquivalenceClasses = null, params Tuple<string, TestValueCategory>[] customValues);
}