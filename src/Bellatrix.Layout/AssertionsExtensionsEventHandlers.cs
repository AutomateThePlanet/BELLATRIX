// <copyright file="AssertionsExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix.Layout
{
    public abstract class AssertionsExtensionsEventHandlers
    {
        public virtual void SubscribeToAll()
        {
            AssertionsExtensions.AssertedAboveOfEvent += AssertedAboveOfEventHandler;
            AssertionsExtensions.AssertedAboveOfNoExpectedValueEvent += AssertedAboveOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedAboveOfBetweenEvent += AssertedAboveOfBetweenEventHandler;
            AssertionsExtensions.AssertedAboveOfGreaterThanEvent += AssertedAboveOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedAboveOfGreaterOrEqualThanEvent += AssertedAboveOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedAboveOfLessThanEvent += AssertedAboveOfLessThanEventHandler;
            AssertionsExtensions.AssertedAboveOfLessOrEqualThanEvent += AssertedAboveOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedAboveOfApproximateEvent += AssertedAboveOfApproximateEventHandler;
            AssertionsExtensions.AssertedBelowOfNoExpectedValueEvent += AssertedBelowOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedBelowOfEvent += AssertedBelowOfEventHandler;
            AssertionsExtensions.AssertedBelowOfBetweenEvent += AssertedBelowOfBetweenEventHandler;
            AssertionsExtensions.AssertedBelowOfGreaterThanEvent += AssertedBelowOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedBelowOfGreaterOrEqualThanEvent += AssertedBelowOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBelowOfLessThanEvent += AssertedBelowOfLessThanEventHandler;
            AssertionsExtensions.AssertedBelowOfLessOrEqualThanEvent += AssertedBelowOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBelowOfApproximateEvent += AssertedBelowOfApproximateEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfNoExpectedValueEvent += AssertedBottomInsideOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfEvent += AssertedBottomInsideOfEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfBetweenEvent += AssertedBottomInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfGreaterThanEvent += AssertedBottomInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfGreaterOrEqualThanEvent += AssertedBottomInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfLessThanEvent += AssertedBottomInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfLessOrEqualThanEvent += AssertedBottomInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfApproximateEvent += AssertedBottomInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedHeightEvent += AssertedHeightEventHandler;
            AssertionsExtensions.AssertedHeightBetweenEvent += AssertedHeightBetweenEventHandler;
            AssertionsExtensions.AssertedHeightLessThanEvent += AssertedHeightLessThanEventHandler;
            AssertionsExtensions.AssertedHeightLessThanOrEqualEvent += AssertedHeightLessThanOrEqualEventHandler;
            AssertionsExtensions.AssertedHeightGreaterThanEvent += AssertedHeightGreaterThanEventHandler;
            AssertionsExtensions.AssertedHeightGreaterThanOrEqualEvent += AssertedHeightGreaterThanOrEqualEventHandler;
            AssertionsExtensions.AssertedHeightApproximateSecondElementEvent += AssertedHeightApproximateSecondElementEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfNoExpectedValueEvent += AssertedLeftInsideOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfEvent += AssertedLeftInsideOfEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfBetweenEvent += AssertedLeftInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfGreaterThanEvent += AssertedLeftInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfGreaterOrEqualThanEvent += AssertedLeftInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfLessThanEvent += AssertedLeftInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfLessOrEqualThanEvent += AssertedLeftInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfApproximateEvent += AssertedLeftInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedLeftOfNoExpectedValueEvent += AssertedLeftOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedLeftOfEvent += AssertedLeftOfEventHandler;
            AssertionsExtensions.AssertedLeftOfBetweenEvent += AssertedLeftOfBetweenEventHandler;
            AssertionsExtensions.AssertedLeftOfGreaterThanEvent += AssertedLeftOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedLeftOfGreaterOrEqualThanEvent += AssertedLeftOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftOfLessThanEvent += AssertedLeftOfLessThanEventHandler;
            AssertionsExtensions.AssertedLeftOfLessOrEqualThanEvent += AssertedLeftOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftOfApproximateEvent += AssertedLeftOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearBottomOfNoExpectedValueEvent += AssertedNearBottomOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedNearBottomOfEvent += AssertedNearBottomOfEventHandler;
            AssertionsExtensions.AssertedNearBottomOfBetweenEvent += AssertedNearBottomOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearBottomOfGreaterThanEvent += AssertedNearBottomOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfGreaterOrEqualThanEvent += AssertedNearBottomOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfLessThanEvent += AssertedNearBottomOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfLessOrEqualThanEvent += AssertedNearBottomOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfApproximateEvent += AssertedNearBottomOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearLeftOfNoExpectedValueEvent += AssertedNearLeftOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedNearLeftOfEvent += AssertedNearLeftOfEventHandler;
            AssertionsExtensions.AssertedNearLeftOfBetweenEvent += AssertedNearLeftOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearLeftOfGreaterThanEvent += AssertedNearLeftOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfGreaterOrEqualThanEvent += AssertedNearLeftOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfLessThanEvent += AssertedNearLeftOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfLessOrEqualThanEvent += AssertedNearLeftOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfApproximateEvent += AssertedNearLeftOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearRightOfNoExpectedValueEvent += AssertedNearRightOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedNearRightOfEvent += AssertedNearRightOfEventHandler;
            AssertionsExtensions.AssertedNearRightOfBetweenEvent += AssertedNearRightOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearRightOfGreaterThanEvent += AssertedNearRightOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfGreaterOrEqualThanEvent += AssertedNearRightOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfLessThanEvent += AssertedNearRightOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfLessOrEqualThanEvent += AssertedNearRightOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfApproximateEvent += AssertedNearRightOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearTopOfNoExpectedValueEvent += AssertedNearTopOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedNearTopOfEvent += AssertedNearTopOfEventHandler;
            AssertionsExtensions.AssertedNearTopOfBetweenEvent += AssertedNearTopOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearTopOfGreaterThanEvent += AssertedNearTopOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfGreaterOrEqualThanEvent += AssertedNearTopOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfLessThanEvent += AssertedNearTopOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfLessOrEqualThanEvent += AssertedNearTopOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfApproximateEvent += AssertedNearTopOfApproximateEventHandler;
            AssertionsExtensions.AssertedRightInsideOfNoExpectedValueEvent += AssertedRightInsideOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedRightInsideOfEvent += AssertedRightInsideOfEventHandler;
            AssertionsExtensions.AssertedRightInsideOfBetweenEvent += AssertedRightInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedRightInsideOfGreaterThanEvent += AssertedRightInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfGreaterOrEqualThanEvent += AssertedRightInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfLessThanEvent += AssertedRightInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfLessOrEqualThanEvent += AssertedRightInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfApproximateEvent += AssertedRightInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedRightOfNoExpectedValueEvent += AssertedRightOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedRightOfEvent += AssertedRightOfEventHandler;
            AssertionsExtensions.AssertedRightOfBetweenEvent += AssertedRightOfBetweenEventHandler;
            AssertionsExtensions.AssertedRightOfGreaterThanEvent += AssertedRightOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedRightOfGreaterOrEqualThanEvent += AssertedRightOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightOfLessThanEvent += AssertedRightOfLessThanEventHandler;
            AssertionsExtensions.AssertedRightOfLessOrEqualThanEvent += AssertedRightOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightOfApproximateEvent += AssertedRightOfApproximateEventHandler;
            AssertionsExtensions.AssertedTopInsideOfNoExpectedValueEvent += AssertedTopInsideOfNoExpectedValueEventHandler;
            AssertionsExtensions.AssertedTopInsideOfEvent += AssertedTopInsideOfEventHandler;
            AssertionsExtensions.AssertedTopInsideOfBetweenEvent += AssertedTopInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedTopInsideOfGreaterThanEvent += AssertedTopInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfGreaterOrEqualThanEvent += AssertedTopInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfLessThanEvent += AssertedTopInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfLessOrEqualThanEvent += AssertedTopInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfApproximateEvent += AssertedTopInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedWidthEvent += AssertedWidthEventHandler;
            AssertionsExtensions.AssertedWidthBetweenEvent += AssertedWidthBetweenEventHandler;
            AssertionsExtensions.AssertedWidthLessThanEvent += AssertedWidthLessThanEventHandler;
            AssertionsExtensions.AssertedWidthLessThanOrEqualEvent += AssertedWidthLessThanOrEqualEventHandler;
            AssertionsExtensions.AssertedWidthGreaterThanEvent += AssertedWidthGreaterThanEventHandler;
            AssertionsExtensions.AssertedWidthGreaterThanOrEqualEvent += AssertedWidthGreaterThanOrEqualEventHandler;
            AssertionsExtensions.AssertedWidthApproximateSecondElementEvent += AssertedWidthApproximateSecondElementEventHandler;
        }

        public virtual void UnsubscribeToAll()
        {
            AssertionsExtensions.AssertedAboveOfEvent -= AssertedAboveOfEventHandler;
            AssertionsExtensions.AssertedAboveOfBetweenEvent -= AssertedAboveOfBetweenEventHandler;
            AssertionsExtensions.AssertedAboveOfGreaterThanEvent -= AssertedAboveOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedAboveOfGreaterOrEqualThanEvent -= AssertedAboveOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedAboveOfLessThanEvent -= AssertedAboveOfLessThanEventHandler;
            AssertionsExtensions.AssertedAboveOfLessOrEqualThanEvent -= AssertedAboveOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedAboveOfApproximateEvent -= AssertedAboveOfApproximateEventHandler;
            AssertionsExtensions.AssertedBelowOfEvent -= AssertedBelowOfEventHandler;
            AssertionsExtensions.AssertedBelowOfBetweenEvent -= AssertedBelowOfBetweenEventHandler;
            AssertionsExtensions.AssertedBelowOfGreaterThanEvent -= AssertedBelowOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedBelowOfGreaterOrEqualThanEvent -= AssertedBelowOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBelowOfLessThanEvent -= AssertedBelowOfLessThanEventHandler;
            AssertionsExtensions.AssertedBelowOfLessOrEqualThanEvent -= AssertedBelowOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBelowOfApproximateEvent -= AssertedBelowOfApproximateEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfEvent -= AssertedBottomInsideOfEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfBetweenEvent -= AssertedBottomInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfGreaterThanEvent -= AssertedBottomInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfGreaterOrEqualThanEvent -= AssertedBottomInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfLessThanEvent -= AssertedBottomInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfLessOrEqualThanEvent -= AssertedBottomInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedBottomInsideOfApproximateEvent -= AssertedBottomInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedHeightEvent -= AssertedHeightEventHandler;
            AssertionsExtensions.AssertedHeightBetweenEvent -= AssertedHeightBetweenEventHandler;
            AssertionsExtensions.AssertedHeightLessThanEvent -= AssertedHeightLessThanEventHandler;
            AssertionsExtensions.AssertedHeightLessThanOrEqualEvent -= AssertedHeightLessThanOrEqualEventHandler;
            AssertionsExtensions.AssertedHeightGreaterThanEvent -= AssertedHeightGreaterThanEventHandler;
            AssertionsExtensions.AssertedHeightGreaterThanOrEqualEvent -= AssertedHeightGreaterThanOrEqualEventHandler;
            AssertionsExtensions.AssertedHeightApproximateSecondElementEvent -= AssertedHeightApproximateSecondElementEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfEvent -= AssertedLeftInsideOfEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfBetweenEvent -= AssertedLeftInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfGreaterThanEvent -= AssertedLeftInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfGreaterOrEqualThanEvent -= AssertedLeftInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfLessThanEvent -= AssertedLeftInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfLessOrEqualThanEvent -= AssertedLeftInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftInsideOfApproximateEvent -= AssertedLeftInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedLeftOfEvent -= AssertedLeftOfEventHandler;
            AssertionsExtensions.AssertedLeftOfBetweenEvent -= AssertedLeftOfBetweenEventHandler;
            AssertionsExtensions.AssertedLeftOfGreaterThanEvent -= AssertedLeftOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedLeftOfGreaterOrEqualThanEvent -= AssertedLeftOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftOfLessThanEvent -= AssertedLeftOfLessThanEventHandler;
            AssertionsExtensions.AssertedLeftOfLessOrEqualThanEvent -= AssertedLeftOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedLeftOfApproximateEvent -= AssertedLeftOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearBottomOfEvent -= AssertedNearBottomOfEventHandler;
            AssertionsExtensions.AssertedNearBottomOfBetweenEvent -= AssertedNearBottomOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearBottomOfGreaterThanEvent -= AssertedNearBottomOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfGreaterOrEqualThanEvent -= AssertedNearBottomOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfLessThanEvent -= AssertedNearBottomOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfLessOrEqualThanEvent -= AssertedNearBottomOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearBottomOfApproximateEvent -= AssertedNearBottomOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearLeftOfEvent -= AssertedNearLeftOfEventHandler;
            AssertionsExtensions.AssertedNearLeftOfBetweenEvent -= AssertedNearLeftOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearLeftOfGreaterThanEvent -= AssertedNearLeftOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfGreaterOrEqualThanEvent -= AssertedNearLeftOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfLessThanEvent -= AssertedNearLeftOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfLessOrEqualThanEvent -= AssertedNearLeftOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearLeftOfApproximateEvent -= AssertedNearLeftOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearRightOfEvent -= AssertedNearRightOfEventHandler;
            AssertionsExtensions.AssertedNearRightOfBetweenEvent -= AssertedNearRightOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearRightOfGreaterThanEvent -= AssertedNearRightOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfGreaterOrEqualThanEvent -= AssertedNearRightOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfLessThanEvent -= AssertedNearRightOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfLessOrEqualThanEvent -= AssertedNearRightOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearRightOfApproximateEvent -= AssertedNearRightOfApproximateEventHandler;
            AssertionsExtensions.AssertedNearTopOfEvent -= AssertedNearTopOfEventHandler;
            AssertionsExtensions.AssertedNearTopOfBetweenEvent -= AssertedNearTopOfBetweenEventHandler;
            AssertionsExtensions.AssertedNearTopOfGreaterThanEvent -= AssertedNearTopOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfGreaterOrEqualThanEvent -= AssertedNearTopOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfLessThanEvent -= AssertedNearTopOfLessThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfLessOrEqualThanEvent -= AssertedNearTopOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedNearTopOfApproximateEvent -= AssertedNearTopOfApproximateEventHandler;
            AssertionsExtensions.AssertedRightInsideOfEvent -= AssertedRightInsideOfEventHandler;
            AssertionsExtensions.AssertedRightInsideOfBetweenEvent -= AssertedRightInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedRightInsideOfGreaterThanEvent -= AssertedRightInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfGreaterOrEqualThanEvent -= AssertedRightInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfLessThanEvent -= AssertedRightInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfLessOrEqualThanEvent -= AssertedRightInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightInsideOfApproximateEvent -= AssertedRightInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedRightOfEvent -= AssertedRightOfEventHandler;
            AssertionsExtensions.AssertedRightOfBetweenEvent -= AssertedRightOfBetweenEventHandler;
            AssertionsExtensions.AssertedRightOfGreaterThanEvent -= AssertedRightOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedRightOfGreaterOrEqualThanEvent -= AssertedRightOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightOfLessThanEvent -= AssertedRightOfLessThanEventHandler;
            AssertionsExtensions.AssertedRightOfLessOrEqualThanEvent -= AssertedRightOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedRightOfApproximateEvent -= AssertedRightOfApproximateEventHandler;
            AssertionsExtensions.AssertedTopInsideOfEvent -= AssertedTopInsideOfEventHandler;
            AssertionsExtensions.AssertedTopInsideOfBetweenEvent -= AssertedTopInsideOfBetweenEventHandler;
            AssertionsExtensions.AssertedTopInsideOfGreaterThanEvent -= AssertedTopInsideOfGreaterThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfGreaterOrEqualThanEvent -= AssertedTopInsideOfGreaterOrEqualThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfLessThanEvent -= AssertedTopInsideOfLessThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfLessOrEqualThanEvent -= AssertedTopInsideOfLessOrEqualThanEventHandler;
            AssertionsExtensions.AssertedTopInsideOfApproximateEvent -= AssertedTopInsideOfApproximateEventHandler;
            AssertionsExtensions.AssertedWidthEvent -= AssertedWidthEventHandler;
            AssertionsExtensions.AssertedWidthBetweenEvent -= AssertedWidthBetweenEventHandler;
            AssertionsExtensions.AssertedWidthLessThanEvent -= AssertedWidthLessThanEventHandler;
            AssertionsExtensions.AssertedWidthLessThanOrEqualEvent -= AssertedWidthLessThanOrEqualEventHandler;
            AssertionsExtensions.AssertedWidthGreaterThanEvent -= AssertedWidthGreaterThanEventHandler;
            AssertionsExtensions.AssertedWidthGreaterThanOrEqualEvent -= AssertedWidthGreaterThanOrEqualEventHandler;
            AssertionsExtensions.AssertedWidthApproximateSecondElementEvent -= AssertedWidthApproximateSecondElementEventHandler;
        }

        protected virtual void AssertedAboveOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedAboveOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBelowOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedBottomInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedHeightEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightBetweenEventHandler(object sender, LayoutElementTwoValuesActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightLessThanEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightLessThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightGreaterThanEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightGreaterThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedHeightApproximateSecondElementEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedLeftOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearBottomOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearLeftOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearRightOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedNearTopOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedRightOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfNoExpectedValueEventHandler(object sender, LayoutTwoElementsNoExpectedActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfBetweenEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfGreaterThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfGreaterOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfLessThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfLessOrEqualThanEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }

        protected virtual void AssertedTopInsideOfApproximateEventHandler(object sender, LayoutTwoElementsActionTwoValuesEventArgs arg)
        {
        }

        protected virtual void AssertedWidthEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthBetweenEventHandler(object sender, LayoutElementTwoValuesActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthLessThanEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthLessThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthGreaterThanEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthGreaterThanOrEqualEventHandler(object sender, LayoutElementActionEventArgs arg)
        {
        }

        protected virtual void AssertedWidthApproximateSecondElementEventHandler(object sender, LayoutTwoElementsActionEventArgs arg)
        {
        }
    }
}