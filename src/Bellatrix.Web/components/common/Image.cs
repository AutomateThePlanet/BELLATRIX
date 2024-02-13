// <copyright file="Image.cs" company="Automate The Planet Ltd.">
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
using System.Web;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public class Image : Component, IComponentSrc, IComponentHeight, IComponentWidth, IComponentLongDesc, IComponentAlt, IComponentSrcSet, IComponentSizes
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Clicking;
    public static event EventHandler<ComponentActionEventArgs> Clicked;

    public override Type ComponentType => GetType();

    public virtual void Click()
    {
        Click(Clicking, Clicked);
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Src => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("src")));

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string LongDesc => string.IsNullOrEmpty(GetAttribute("longdesc")) ? null : GetAttribute("longdesc");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Alt => string.IsNullOrEmpty(GetAttribute("alt")) ? null : GetAttribute("alt");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string SrcSet => string.IsNullOrEmpty(GetAttribute("srcset")) ? null : GetAttribute("srcset");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Sizes => string.IsNullOrEmpty(GetAttribute("sizes")) ? null : GetAttribute("sizes");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual int? Height => GetHeightAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual int? Width => GetWidthAttribute();
}