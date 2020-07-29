// <copyright file="DynamicTestCasesAssertionExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.DynamicTestCases;

namespace Bellatrix.Layout
{
    public class DynamicTestCasesAssertionExtensions : AssertionsExtensionsEventHandlers
    {
        protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        protected override void AssertedAboveOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px above of {arg.SecondElement.ElementName}.");

        protected override void AssertedAboveOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px above of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedBelowOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px below of {arg.SecondElement.ElementName}.");

        protected override void AssertedBelowOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px below of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedBottomInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedBottomInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px bottom inside of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedHeightEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is {arg.ActionValue} px.");

        protected override void AssertedHeightBetweenEventHandler(object sender, LayoutElementTwoValuesActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is {arg.ActionValue}-{arg.SecondActionValue} px.");

        protected override void AssertedHeightLessThanEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is <{arg.ActionValue} px.");

        protected override void AssertedHeightLessThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is <={arg.ActionValue} px.");

        protected override void AssertedHeightGreaterThanEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is >{arg.ActionValue} px.");

        protected override void AssertedHeightGreaterThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is >={arg.ActionValue} px.");

        protected override void AssertedHeightApproximateSecondElementEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is ~{arg.ActionValue}% of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px left inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px left inside of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px left of {arg.SecondElement.ElementName}.");

        protected override void AssertedLeftOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px left of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedNearBottomOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearBottomOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near bottom of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedNearLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px near left of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearLeftOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near left of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedNearRightOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px near right of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearRightOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near right of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedNearTopOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px near top of {arg.SecondElement.ElementName}.");

        protected override void AssertedNearTopOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px near top of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedRightInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px right inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px right inside of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedRightOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px right of {arg.SecondElement.ElementName}.");

        protected override void AssertedRightOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px right of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedTopInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue}-{arg.SecondActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >{arg.ActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is >={arg.ActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <{arg.ActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is <={arg.ActionValue} px top inside of {arg.SecondElement.ElementName}.");

        protected override void AssertedTopInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is {arg.ActionValue} px top inside of {arg.SecondElement.ElementName}. ({arg.SecondActionValue}% tolerance)");

        protected override void AssertedWidthEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is {arg.ActionValue} px.");

        protected override void AssertedWidthBetweenEventHandler(object sender, LayoutElementTwoValuesActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is {arg.ActionValue}-{arg.SecondActionValue} px.");

        protected override void AssertedWidthLessThanEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is <{arg.ActionValue} px.");

        protected override void AssertedWidthLessThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is <={arg.ActionValue} px.");

        protected override void AssertedWidthGreaterThanEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is >{arg.ActionValue} px.");

        protected override void AssertedWidthGreaterThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is >={arg.ActionValue} px.");

        protected override void AssertedWidthApproximateSecondElementEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
            => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is ~{arg.ActionValue}% of {arg.SecondElement.ElementName}.");
    }
}