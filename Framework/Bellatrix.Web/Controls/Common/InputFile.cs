// <copyright file="InputFile.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class InputFile : Element, IElementRequired, IElementMultiple, IElementAccept
    {
        public static Action<InputFile, string> OverrideUploadGlobally;
        public static Func<InputFile, bool> OverrideIsRequiredGlobally;
        public static Func<InputFile, bool> OverrideIsMultipleGlobally;
        public static Func<InputFile, string> OverrideAcceptGlobally;

        public static Action<InputFile, string> OverrideUploadLocally;
        public static Func<InputFile, bool> OverrideIsRequiredLocally;
        public static Func<InputFile, bool> OverrideIsMultipleLocally;
        public static Func<InputFile, string> OverrideAcceptLocally;

        public static event EventHandler<ElementActionEventArgs> Uploading;
        public static event EventHandler<ElementActionEventArgs> Uploaded;

        public static new void ClearLocalOverrides()
        {
            OverrideUploadLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideIsMultipleLocally = null;
            OverrideAcceptLocally = null;
        }

        public override Type ElementType => GetType();

        public void Upload(string file)
        {
            var action = InitializeAction(this, OverrideUploadGlobally, OverrideUploadLocally, DefaultUpload);
            action(file);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsRequired
        {
            get
            {
                var action = InitializeAction(this, OverrideIsRequiredGlobally, OverrideIsRequiredLocally, DefaultGetRequired);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsMultiple
        {
            get
            {
                var action = InitializeAction(this, OverrideIsMultipleGlobally, OverrideIsMultipleLocally, DefaultGetMultiple);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Accept
        {
            get
            {
                var action = InitializeAction(this, OverrideAcceptGlobally, OverrideAcceptLocally, DefaultGetAccept);
                return action();
            }
        }

        protected virtual void DefaultUpload(InputFile inputFile, string filePath)
        {
            Uploading?.Invoke(this, new ElementActionEventArgs(inputFile));

            WrappedElement.SendKeys(filePath);

            Uploaded?.Invoke(this, new ElementActionEventArgs(inputFile));
        }

        protected virtual bool DefaultGetRequired(InputFile textArea) => base.DefaultGetRequired(textArea);

        protected virtual bool DefaultGetMultiple(InputFile textArea) => !string.IsNullOrEmpty(GetAttribute("multiple"));

        protected virtual string DefaultGetAccept(InputFile textArea) => string.IsNullOrEmpty(GetAttribute("accept")) ? null : GetAttribute("accept");
    }
}