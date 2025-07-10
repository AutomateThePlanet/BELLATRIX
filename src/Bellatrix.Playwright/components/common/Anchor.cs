﻿// <copyright file="Anchor.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using System.Web;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public class Anchor : Component, IComponentHref, IComponentInnerText, IComponentInnerHtml, IComponentRel, IComponentTarget
{
    public static event EventHandler<ComponentActionEventArgs> Clicking;
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;

    public override Type ComponentType => GetType();

    public virtual void Click(LocatorClickOptions options = null)
    {
        DefaultClick(Clicking, Clicked, options);
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Href => new Uri(new Uri(WrappedBrowser.CurrentPage.Url), HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("href")))).AbsoluteUri;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string InnerText => GetInnerText();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string InnerHtml => GetInnerHtmlAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Target => GetAttribute("target");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Rel => GetAttribute("rel");
}