// <copyright file="Screen.cs" company="Automate The Planet Ltd.">
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

using System.Drawing;
using Bellatrix.Layout;
using Bellatrix.Playwright.Services;

namespace Bellatrix.Playwright;

internal class Screen : ILayoutComponent
{
    internal Screen()
    {
        var browserService = ServicesCollection.Current.Resolve<BrowserService>();
        Location = new Point(0, 0);
        var nativeSize = browserService.WrappedBrowser.CurrentPage.ViewportSize;
        Size = new(nativeSize.Width, nativeSize.Height);
    }

    public Point Location { get; }

    public Size Size { get; }

    public string ComponentName => "Screen";
}
