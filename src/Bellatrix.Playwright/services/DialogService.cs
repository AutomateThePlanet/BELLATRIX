// <copyright file="DialogService.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;
using System.Threading.Tasks;
using Bellatrix.Playwright.Settings;
using System.Threading;
using Bellatrix.Playwright.Settings.Extensions;

namespace Bellatrix.Playwright;

public class DialogService : WebService
{
    public Dialog Dialog { get; internal set; }

    public DialogService(WrappedBrowser wrappedBrowser)
        : base(wrappedBrowser)
    {
    }

    /// <summary>
    /// Unless you use this method and wrap your logic that will trigger a dialog, Playwright will automatically dismiss all dialogs.
    /// </summary>
    /// <param name="timeout">in milliseconds</param>
    public Dialog RunAndWaitForDialog(Action action, int? timeout = null)
    {
        CurrentPage.OnceDialog((dialog) => Dialog = dialog);

        try
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(timeout ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().ActionTimeoutWhenHandlingDialogs);

            try
            {
                Task.Run(action).WaitAsync(cancellationTokenSource.Token).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Action timed out and was cancelled. Possible reason could be dialog presence before action completion.");
            }
        }
        catch
        {

        }

        return Dialog;
    }

    public void Accept(string promptText = null)
    {
        Dialog.Accept(promptText);
    }

    public void Dismiss()
    {
        Dialog.Dismiss();
    }

    public string GetMessage()
    {
        return Dialog.Message;
    }

    public void AddDialogHandler(EventHandler<Dialog> handler)
    {
        CurrentPage.OnDialog += handler;
    }

    public void RemoveDialogHandler(EventHandler<Dialog> handler)
    {
        CurrentPage.OnDialog -= handler;
    }

    public void OnceDialog(Action<Dialog> handler)
    {
        CurrentPage.OnceDialog(handler);
    }
}