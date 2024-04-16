// <copyright file="BrowserContext.Events.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Playwright.SyncPlaywright;

public partial class BrowserContext
{
    public event EventHandler<BrowserContext> OnClose
    {
        add => WrappedBrowserContext.Close += (sender, e) => value(sender, this);
        remove => WrappedBrowserContext.Close -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IConsoleMessage> OnConsole
    {
        add => WrappedBrowserContext.Console += value;
        remove => WrappedBrowserContext.Console -= value;
    }

    public event EventHandler<Dialog> OnDialog
    {
        add => WrappedBrowserContext.Dialog += (sender, e) => value(sender, new Dialog(e));
        remove => WrappedBrowserContext.Dialog -= (sender, e) => value(sender, new Dialog(e));
    }

    public event EventHandler<BrowserContext> OnPage
    {
        add => WrappedBrowserContext.Page += (sender, e) => value(sender, this);
        remove => WrappedBrowserContext.Page -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IWebError> OnWebError
    {
        add => WrappedBrowserContext.WebError += value;
        remove => WrappedBrowserContext.WebError -= value;
    }

    public event EventHandler<IRequest> OnRequest
    {
        add => WrappedBrowserContext.Request += value;
        remove => WrappedBrowserContext.Request -= value;
    }

    public event EventHandler<IRequest> OnRequestFailed
    {
        add => WrappedBrowserContext.RequestFailed += value;
        remove => WrappedBrowserContext.RequestFailed -= value;
    }

    public event EventHandler<IRequest> OnRequestFinished
    {
        add => WrappedBrowserContext.RequestFinished += value;
        remove => WrappedBrowserContext.RequestFinished -= value;
    }

    public event EventHandler<IResponse> OnResponse
    {
        add => WrappedBrowserContext.Response += value;
        remove => WrappedBrowserContext.Response -= value;
    }
}
