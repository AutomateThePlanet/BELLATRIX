// <copyright file="By.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using Bellatrix.Playwright.Services.Browser;

namespace Bellatrix.Playwright;

public abstract class FindStrategy
{
    public FindStrategy(string value) => Value = value;

    public string Value { get; }

    public abstract ILocator Convert(IPage searchContext);

    public abstract ILocator Convert(ILocator searchContext);

    protected WrappedBrowser WrappedBrowser => ServicesCollection.Current.Resolve<WrappedBrowser>();
}