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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using OpenQA.Selenium;

namespace Bellatrix.Web;

public class DialogService : WebService
{
    public DialogService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok)
    {
        var alert = WrappedDriver.SwitchTo().Alert();
        action?.Invoke(alert);
        if (dialogButton == DialogButton.Ok)
        {
            alert.Accept();
            WrappedDriver.SwitchTo().DefaultContent();
        }
        else
        {
            alert.Dismiss();
            WrappedDriver.SwitchTo().DefaultContent();
        }
    }

    public string GetText()
    {
        var alert = WrappedDriver.SwitchTo().Alert();

        return alert.Text;
    }
}