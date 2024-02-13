// <copyright file="InputFile.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public class InputFile : Component, IComponentRequired, IComponentMultiple, IComponentAccept
{
    public static event EventHandler<ComponentActionEventArgs> Uploading;
    public static event EventHandler<ComponentActionEventArgs> Uploaded;

    public override Type ComponentType => GetType();

    public virtual void Upload(string file)
    {
        DefaultUpload(file);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsMultiple => !string.IsNullOrEmpty(GetAttribute("multiple"));

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Accept => string.IsNullOrEmpty(GetAttribute("accept")) ? null : GetAttribute("accept");

    protected virtual void DefaultUpload(string filePath)
    {
        Uploading?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.SendKeys(filePath);

        Uploaded?.Invoke(this, new ComponentActionEventArgs(this));
    }
}