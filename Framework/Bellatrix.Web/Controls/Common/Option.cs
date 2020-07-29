// <copyright file="Option.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using Bellatrix.Web.Contracts;

namespace Bellatrix.Web
{
    public class Option : Element, IElementInnerText, IElementValue, IElementDisabled, IElementSelected
    {
        public static Func<Option, string> OverrideInnerTextGlobally;
        public static Func<Option, bool> OverrideIsSelectedGlobally;
        public static Func<Option, bool> OverrideIsDisabledGlobally;
        public static Func<Option, string> OverrideValueGlobally;

        public static Func<Option, string> OverrideInnerTextLocally;
        public static Func<Option, bool> OverrideIsSelectedLocally;
        public static Func<Option, bool> OverrideIsDisabledLocally;
        public static Func<Option, string> OverrideValueLocally;

        public static new void ClearLocalOverrides()
        {
            OverrideInnerTextLocally = null;
            OverrideIsSelectedLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
        }

        public override Type ElementType => GetType();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerText
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerTextGlobally, OverrideInnerTextLocally, DefaultInnerText);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Value
        {
            get
            {
                var action = InitializeAction(this, OverrideValueGlobally, OverrideValueLocally, DefaultGetValue);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsSelected
        {
            get
            {
                var action = InitializeAction(this, OverrideIsSelectedGlobally, OverrideIsSelectedLocally, DefaultIsSelected);
                return action();
            }
        }

        protected virtual string DefaultInnerText(Option option) => WrappedElement.Text.Replace("\r\n", string.Empty);

        protected virtual bool DefaultIsSelected(Option option) => WrappedElement.Selected;

        protected virtual string DefaultGetValue(Option option) => WrappedElement.GetAttribute("value");

        protected virtual bool DefaultIsDisabled(Option option) => !WrappedElement.Enabled;
    }
}