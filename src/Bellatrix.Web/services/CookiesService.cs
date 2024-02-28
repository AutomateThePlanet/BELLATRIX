// <copyright file="CookiesService.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Bellatrix.Web;

public class CookiesService : WebService
{
    public CookiesService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public void AddCookie(string cookieName, string cookieValue, string path = "/")
    {
        cookieValue = Uri.UnescapeDataString(cookieValue);
        var cookie = new Cookie(cookieName, cookieValue, path);
        WrappedDriver.Manage().Cookies.AddCookie(cookie);
    }

    public void AddCookie(System.Net.Cookie cookieToAdd)
    {
        var cookieValue = Uri.UnescapeDataString(cookieToAdd.Value);

        Cookie updatedCookie = new ReturnedCookie(
            cookieToAdd.Name,
            cookieValue,
            cookieToAdd.Domain,
            cookieToAdd.Path,
            cookieToAdd.Expires == default ? null : (DateTime?)cookieToAdd.Expires,
            cookieToAdd.Secure,
            cookieToAdd.HttpOnly);

        WrappedDriver.Manage().Cookies.AddCookie(updatedCookie);
    }

    public void DeleteAllCookies() => WrappedDriver.Manage().Cookies.DeleteAllCookies();

    public void DeleteCookie(string cookieName) => WrappedDriver.Manage().Cookies.DeleteCookieNamed(cookieName);

    public List<Cookie> GetAllCookies()
    {
        var cookies = new List<Cookie>();
        foreach (var currentCookie in WrappedDriver.Manage().Cookies.AllCookies)
        {
            cookies.Add(currentCookie);
        }

        return cookies;
    }

    public string GetCookie(string cookieName)
    {
        var cookie = WrappedDriver.Manage().Cookies.GetCookieNamed(cookieName);
        if (cookie != null)
        {
            return cookie.Value;
        }

        return string.Empty;
    }
}