// <copyright file="CookiesServiceSteps.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web
{
    [Binding]
    public class CookiesServiceSteps : WebSteps
    {
        [When(@"I add cookie name = (.*) value = (.*)")]
        public void WhenISetCookie(string name, string value)
        {
            App.CookieService.AddCookie(name, value);
        }

        [When(@"I delete cookie name = (.*)")]
        [When(@"I delete cookie (.*)")]
        public void WhenIDeleteCookie(string name)
        {
            App.CookieService.DeleteCookie(name);
        }

        [When(@"I delete all cookies")]
        public void WhenIDeleteAllCookies()
        {
            App.CookieService.DeleteAllCookies();
        }
    }
}
