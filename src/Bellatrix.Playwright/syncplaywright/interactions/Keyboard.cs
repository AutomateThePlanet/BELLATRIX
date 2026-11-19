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
        WrappedKeyboard.DownAsync(key).GetAwaiter().GetResult();
    }

    public void InsertText(string text)
    {
        WrappedKeyboard.InsertTextAsync(text).GetAwaiter().GetResult();
    }

    public void Press(string key, KeyboardPressOptions options = null)
    {
        WrappedKeyboard.PressAsync(key, options).GetAwaiter().GetResult();
    }

    public void Type(string text, KeyboardTypeOptions options = null)
    {
        WrappedKeyboard.TypeAsync(text, options).GetAwaiter().GetResult();
    }

    public void Up(string key)
    {
        WrappedKeyboard.UpAsync(key).GetAwaiter().GetResult();
    }
}
