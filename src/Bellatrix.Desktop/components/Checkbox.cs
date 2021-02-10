// <copyright file="Checkbox.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop
{
   public class CheckBox : Element, IElementDisabled, IElementChecked
    {
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Checking;
        public static event EventHandler<ElementActionEventArgs> Checked;
        public static event EventHandler<ElementActionEventArgs> Unchecking;
        public static event EventHandler<ElementActionEventArgs> Unchecked;

        public void Check(bool isChecked = true)
        {
            if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
            {
                Click(Checking, Checked);
            }
        }

        public void Uncheck()
        {
            if (WrappedElement.Selected)
            {
                Click(Unchecking, Unchecked);
            }
        }

        public void Hover()
        {
            Hover(Hovering, Hovered);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled => GetIsDisabled();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsChecked => WrappedElement.Selected;
    }
}