// <copyright file="Frame.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;
using System.Diagnostics;

namespace Bellatrix.Playwright;

public class Frame : Component, IComponentUrl, IComponentInnerHtml, IComponentDisabled
{
    public static event EventHandler<ComponentActionEventArgs> SettingUrl;
    public static event EventHandler<ComponentActionEventArgs> UrlSet;

    public string Name => GetAttribute("name");

    public override TComponent As<TComponent>()
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = this.By;

        if (component is not Frame)
        {
            component.WrappedElement = new WebElement(this.WrappedElement.Page, this.WrappedElement.WrappedLocator);
        }
        else
        {
            component.WrappedElement = this.WrappedElement;
        }

        return component;
    }

    public virtual string GetUrl()
    {
        return DefaultGetValue();
    }

    public virtual void SetUrl(string url)
    {
        SetValue(SettingUrl, UrlSet, url);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string InnerHtml => GetInnerHtmlAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    public ComponentsList<Frame> ChildFrames => this.CreateAllByXpath<Frame>("//iframe");
}