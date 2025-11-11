// <copyright file="Mouse.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.Services.WebApi;

namespace Bellatrix.Playwright.SyncPlaywright;

public class Mouse
{
    public Mouse(IMouse mouse)
    {
        WrappedMouse = mouse;
    }

    public IMouse WrappedMouse { get; internal set; }

    public void Click(float x, float y, MouseClickOptions options = null)
    {
        WrappedMouse.ClickAsync(x, y, options).SyncResult();
    }

    public void DblClick(float x, float y, MouseDblClickOptions options = null)
    {
        WrappedMouse.DblClickAsync(x, y, options).SyncResult();
    }

    public void Down(MouseDownOptions options = null)
    {
        WrappedMouse.DownAsync(options).SyncResult();
    }

    public void Move(float x, float y, MouseMoveOptions options = null)
    {
        WrappedMouse.MoveAsync(x, y, options).SyncResult();
    }

    public void Up(MouseUpOptions options = null)
    {
        WrappedMouse.UpAsync(options).SyncResult();
    }

    public void Wheel(float deltaX, float deltaY)
    {
        WrappedMouse.WheelAsync(deltaX, deltaY).SyncResult();
    }
}
