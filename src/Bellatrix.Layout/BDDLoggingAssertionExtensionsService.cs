// <copyright file="BDDLoggingAssertionExtensionsService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
namespace Bellatrix.Layout;

public class BDDLoggingAssertionExtensionsService : AssertionsExtensionsEventHandlers
{
    protected override void AssertedAboveOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px above of {arg.SecondElement.ComponentName}.");

    protected override void AssertedAboveOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px above of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedBelowOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px below of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBelowOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px below of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedBottomInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedBottomInsideOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px bottom inside of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedHeightEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is {arg.ActionValue} px.");

    protected override void AssertedHeightBetweenEventHandler(object sender, LayoutComponentTwoValuesActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is {arg.ActionValue}-{arg.SecondActionValue} px.");

    protected override void AssertedHeightLessThanEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is <{arg.ActionValue} px.");

    protected override void AssertedHeightLessThanOrEqualEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is <={arg.ActionValue} px.");

    protected override void AssertedHeightGreaterThanEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is >{arg.ActionValue} px.");

    protected override void AssertedHeightGreaterThanOrEqualEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is >={arg.ActionValue} px.");

    protected override void AssertedHeightApproximateSecondElementEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} height is ~{arg.ActionValue}% of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftInsideOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px left inside of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedLeftOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px left of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedNearBottomOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearBottomOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near bottom of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedNearLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px near left of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearLeftOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near left of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedNearRightOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px near right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearRightOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near right of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedNearTopOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px near top of {arg.SecondElement.ComponentName}.");

    protected override void AssertedNearTopOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px near top of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedRightInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightInsideOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px right inside of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedRightOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px right of {arg.SecondElement.ComponentName}.");

    protected override void AssertedRightOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px right of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedTopInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoComponentsNoExpectedActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfBetweenEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue}-{arg.SecondActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfGreaterThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >{arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is >={arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfLessThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <{arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is <={arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}.");

    protected override void AssertedTopInsideOfApproximateEventHandler(object sender, LayoutTwoComponentsActionTwoValuesEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} is {arg.ActionValue} px top inside of {arg.SecondElement.ComponentName}. ({arg.SecondActionValue}% tolerance)");

    protected override void AssertedWidthEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is {arg.ActionValue} px.");

    protected override void AssertedWidthBetweenEventHandler(object sender, LayoutComponentTwoValuesActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is {arg.ActionValue}-{arg.SecondActionValue} px.");

    protected override void AssertedWidthLessThanEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is <{arg.ActionValue} px.");

    protected override void AssertedWidthLessThanOrEqualEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is <={arg.ActionValue} px.");

    protected override void AssertedWidthGreaterThanEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is >{arg.ActionValue} px.");

    protected override void AssertedWidthGreaterThanOrEqualEventHandler(object sender, LayoutComponentActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is >={arg.ActionValue} px.");

    protected override void AssertedWidthApproximateSecondElementEventHandler(object sender, LayoutTwoComponentsActionEventArgs arg)
        => Logger.LogInformation($"Assert {arg.Element.ComponentName} width is ~{arg.ActionValue}% of {arg.SecondElement.ComponentName}.");
}