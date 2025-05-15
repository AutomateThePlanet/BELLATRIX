// <copyright file="Keyboard.cs" company="Automate The Planet Ltd.">
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

public class Keyboard
{
    public Keyboard(IKeyboard keyboard)
    {
        WrappedKeyboard = keyboard;
    }

    public IKeyboard WrappedKeyboard { get; internal set; }

    public void Down(string key)
    {
        WrappedKeyboard.DownAsync(key).SyncResult();
    }

    public void InsertText(string text)
    {
        WrappedKeyboard.InsertTextAsync(text).SyncResult();
    }

    public void Press(string key, KeyboardPressOptions options = null)
    {
        WrappedKeyboard.PressAsync(key, options).SyncResult();
    }

    public void Type(string text, KeyboardTypeOptions options = null)
    {
        WrappedKeyboard.TypeAsync(text, options).SyncResult();
    }

    public void Up(string key)
    {
        WrappedKeyboard.UpAsync(key).SyncResult();
    }
}
