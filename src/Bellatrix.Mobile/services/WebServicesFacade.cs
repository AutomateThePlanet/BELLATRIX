// <copyright file="WebServicesFacade.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web;

namespace Bellatrix.Mobile.Services;

public class WebServicesFacade
{
    private ServicesCollection _childContainer;
    public WebServicesFacade(ServicesCollection childContainer)
    {
        _childContainer = childContainer;
    }

    public BrowserService BrowserService => _childContainer.Resolve<BrowserService>();
    public CookiesService CookiesService => _childContainer.Resolve<CookiesService>();
    public DialogService DialogService => _childContainer.Resolve<DialogService>();
    public JavaScriptService JavaScriptService => _childContainer.Resolve<JavaScriptService>();
    public NavigationService NavigationService => _childContainer.Resolve<NavigationService>();
    public ComponentCreateService ComponentCreateService => _childContainer.Resolve<ComponentCreateService>();
    public InteractionsService InteractionsService => _childContainer.Resolve<InteractionsService>();
}
