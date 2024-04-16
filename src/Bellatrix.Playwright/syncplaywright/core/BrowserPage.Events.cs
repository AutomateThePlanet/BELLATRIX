// <copyright file="BrowserPage.Events.cs" company="Automate The Planet Ltd.">
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

using static Bellatrix.Playwright.Utilities.EventHandlerUtilities;

namespace Bellatrix.Playwright.SyncPlaywright;

public partial class BrowserPage
{
    public event EventHandler<BrowserPage> OnClose
    {
        add => WrappedPage.Close += (sender, e) => value(sender, this);
        remove => WrappedPage.Close -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IConsoleMessage> OnConsole
    {
        add => WrappedPage.Console += value;
        remove => WrappedPage.Console -= value;
    }

    public event EventHandler<BrowserPage> OnCrash
    {
        add => WrappedPage.Crash += (sender, e) => value(sender, this);
        remove => WrappedPage.Crash -= (sender, e) => value(sender, this);
    }

    public event EventHandler<Dialog> OnDialog
    {
        add => WrappedPage.Dialog += (sender, e) => value(sender, new Dialog(e));
        remove => WrappedPage.Dialog -= (sender, e) => value(sender, new Dialog(e));
    }

    public event EventHandler<BrowserPage> OnDOMContentLoaded
    {
        add => WrappedPage.DOMContentLoaded += (sender, e) => value(sender, this);
        remove => WrappedPage.DOMContentLoaded -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IDownload> OnDownload
    {
        add => WrappedPage.Download += value;
        remove => WrappedPage.Download -= value;
    }

    public event EventHandler<IFileChooser> OnFileChooser
    {
        add => WrappedPage.FileChooser += value;
        remove => WrappedPage.FileChooser -= value;
    }

    public event EventHandler<IFrame> OnFrameAttached
    {
        add => WrappedPage.FrameAttached += value;
        remove => WrappedPage.FrameAttached -= value;
    }

    public event EventHandler<IFrame> OnFrameDetached
    {
        add => WrappedPage.FrameDetached += value;
        remove => WrappedPage.FrameDetached -= value;
    }

    public event EventHandler<IFrame> OnFrameNavigated
    {
        add => WrappedPage.FrameNavigated += value;
        remove => WrappedPage.FrameNavigated -= value;
    }

    public event EventHandler<BrowserPage> OnLoad
    {
        add => WrappedPage.Load += (sender, e) => value(sender, this);
        remove => WrappedPage.Load -= (sender, e) => value(sender, this);
    }

    public event EventHandler<string> OnPageError
    {
        add => WrappedPage.PageError += value;
        remove => WrappedPage.PageError -= value;
    }

    public event EventHandler<BrowserPage> OnPopup
    {
        add => WrappedPage.Popup += (sender, e) => value(sender, this);
        remove => WrappedPage.Popup -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IRequest> OnRequest
    {
        add => WrappedPage.Request += value;
        remove => WrappedPage.Request -= value;
    }

    public event EventHandler<IRequest> OnRequestFailed
    {
        add => WrappedPage.RequestFailed += value;
        remove => WrappedPage.RequestFailed -= value;
    }

    public event EventHandler<IRequest> OnRequestFinished
    {
        add => WrappedPage.RequestFinished += value;
        remove => WrappedPage.RequestFinished -= value;
    }

    public event EventHandler<IResponse> OnResponse
    {
        add => WrappedPage.Response += value;
        remove => WrappedPage.Response -= value;
    }

    public event EventHandler<IWebSocket> OnWebSocket
    {
        add => WrappedPage.WebSocket += value;
        remove => WrappedPage.WebSocket -= value;
    }

    public event EventHandler<IWorker> OnWorker
    {
        add => WrappedPage.Worker += value;
        remove => WrappedPage.Worker -= value;
    }

    public void OnceClose(Action<BrowserPage> action)
    {
        EventHandler<IPage> handler = (sender, page) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Close));
            action(new BrowserPage(page));
        };

        WrappedPage.Close += handler;
    }

    public void OnceConsole(Action<IConsoleMessage> action)
    {
        EventHandler<IConsoleMessage> handler = (sender, consoleMessage) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Console));
            action(consoleMessage);
        };

        WrappedPage.Console += handler;
    }

    public void OnceCrash(Action<BrowserPage> action)
    {
        EventHandler<IPage> handler = (sender, page) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Crash));
            action(new BrowserPage(page));
        };

        WrappedPage.Crash += handler;
    }

    public void OnceDialog(Action<Dialog> action)
    {
        EventHandler<IDialog> handler = (sender, dialog) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Dialog));
            action(new Dialog(dialog));
        };

        WrappedPage.Dialog += handler;
    }

    public void OnceDOMContentLoaded(Action<BrowserPage> action)
    {
        EventHandler<IPage> handler = (sender, page) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.DOMContentLoaded));
            action(new BrowserPage(page));
        };

        WrappedPage.DOMContentLoaded += handler;
    }

    public void OnceDownload(Action<IDownload> action)
    {
        EventHandler<IDownload> handler = (sender, download) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Download));
            action(download);
        };

        WrappedPage.Download += handler;
    }

    public void OnceFileChooser(Action<IFileChooser> action)
    {
        EventHandler<IFileChooser> handler = (sender, fileChooser) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.FileChooser));
            action(fileChooser);
        };

        WrappedPage.FileChooser += handler;
    }

    public void OnceFrameAttached(Action<IFrame> action)
    {
        EventHandler<IFrame> handler = (sender, frame) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.FrameAttached));
            action(frame);
        };

        WrappedPage.FrameAttached += handler;
    }

    public void OnceFrameDetached(Action<IFrame> action)
    {
        EventHandler<IFrame> handler = (sender, frame) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.FrameDetached));
            action(frame);
        };

        WrappedPage.FrameDetached += handler;
    }

    public void OnceFrameNavigated(Action<IFrame> action)
    {
        EventHandler<IFrame> handler = (sender, frame) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.FrameNavigated));
            action(frame);
        };

        WrappedPage.FrameNavigated += handler;
    }

    public void OnceLoad(Action<BrowserPage> action)
    {
        EventHandler<IPage> handler = (sender, page) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Load));
            action(new BrowserPage(page));
        };

        WrappedPage.Load += handler;
    }

    public void OncePageError(Action<string> action)
    {
        EventHandler<string> handler = (sender, error) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.PageError));
            action(error);
        };

        WrappedPage.PageError += handler;
    }

    public void OncePopup(Action<BrowserPage> action)
    {
        EventHandler<IPage> handler = (sender, page) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Popup));
            action(new BrowserPage(page));
        };

        WrappedPage.Popup += handler;
    }


    public void OnceRequestFailed(Action<IRequest> action)
    {
        EventHandler<IRequest> handler = (sender, request) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.RequestFailed));
            action(request);
        };

        WrappedPage.RequestFailed += handler;
    }


    public void OnceRequestFinished(Action<IRequest> action)
    {
        EventHandler<IRequest> handler = (sender, request) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.RequestFinished));
            action(request);
        };

        WrappedPage.RequestFinished += handler;
    }

    public void OnceRequest(Action<IRequest> action)
    {
        EventHandler<IRequest> handler = (sender, request) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Request));
            action(request);
        };

        WrappedPage.Request += handler;
    }

    public void OnceResponse(Action<IResponse> action)
    {
        EventHandler<IResponse> handler = (sender, response) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Response));
            action(response);
        };

        WrappedPage.Response += handler;
    }

    public void OnceWebSocket(Action<IWebSocket> action)
    {
        EventHandler<IWebSocket> handler = (sender, webSocket) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.WebSocket));
            action(webSocket);
        };

        WrappedPage.WebSocket += handler;
    }

    public void OnceWorker(Action<IWorker> action)
    {
        EventHandler<IWorker> handler = (sender, worker) =>
        {
            Detach(WrappedPage, nameof(WrappedPage.Worker));
            action(worker);
        };

        WrappedPage.Worker += handler;
    }
}
