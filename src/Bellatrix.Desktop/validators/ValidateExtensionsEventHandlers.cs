// <copyright file="ValidateExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Events;
using ElementNotFulfillingValidateConditionEventArgs = Bellatrix.Desktop.Validates.ElementNotFulfillingValidateConditionEventArgs;

namespace Bellatrix.Desktop
{
    public abstract class ValidateExtensionsEventHandlers
    {
        public virtual void SubscribeToAll()
        {
            ValidateControlExtensions.ValidatedIsCheckedEvent += ValidatedIsCheckedEventHandler;
            ValidateControlExtensions.ValidatedIsNotCheckedEvent += ValidatedIsNotCheckedEventHandler;
            ValidateControlExtensions.ValidatedDateIsEvent += ValidatedDateIsEventHandler;
            ValidateControlExtensions.ValidatedIsDisabledEvent += ValidatedIsDisabledEventHandler;
            ValidateControlExtensions.ValidatedIsNotDisabledEvent += ValidatedIsNotDisabledEventHandler;
            ValidateControlExtensions.ValidatedInnerTextIsEvent += ValidatedInnerTextIsEventHandler;
            ValidateControlExtensions.ValidatedIsSelectedEvent += ValidatedIsSelectedEventHandler;
            ValidateControlExtensions.ValidatedIsNotSelectedEvent += ValidatedIsNotSelectedEventHandler;
            ValidateControlExtensions.ValidatedTextIsNullEvent += ValidatedTextIsNullEventHandler;
            ValidateControlExtensions.ValidatedTextIsEvent += ValidatedTextIsEventHandler;
            ValidateControlExtensions.ValidatedTimeIsEvent += ValidatedTimeIsEventHandler;
            ValidateControlExtensions.ValidatedIsVisibleEvent += ValidatedIsVisibleEventHandler;
            ValidateControlExtensions.ValidatedIsNotVisibleEvent += ValidatedIsNotVisibleEventHandler;
            ValidateControlExtensions.ValidatedExceptionThrowedEvent += ValidatedExceptionThrowedEventHandler;
        }

        protected virtual void ValidatedExceptionThrowedEventHandler(object sender, ElementNotFulfillingValidateConditionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedTimeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedInnerTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedDateIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void ValidatedIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }
    }
}
