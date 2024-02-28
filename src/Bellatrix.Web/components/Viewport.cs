// <copyright file="Viewport.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Layout;

namespace Bellatrix.Web;

internal class Viewport : ILayoutComponent
{
    internal Viewport()
    {
        var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        Location = new Point(0, 0);
        var viewportWidth = int.Parse(javaScriptService.Execute("return Math.max(document.documentElement.clientWidth, window.innerWidth || 0);").ToString());
        var viewportHeight = int.Parse(javaScriptService.Execute("return Math.max(document.documentElement.clientHeight, window.innerHeight || 0);").ToString());
        Size = new Size(viewportWidth, viewportHeight);
    }

    public Point Location { get; }

    public Size Size { get; }

    public string ComponentName => "Viewport";
}