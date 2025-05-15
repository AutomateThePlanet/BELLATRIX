// <copyright file="CookiesService.cs" company="Automate The Planet Ltd.">
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

using AutoMapper;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;

namespace Bellatrix.Playwright;

public class CookiesService : WebService
{
    public CookiesService(WrappedBrowser wrappedBrowser)
        : base(wrappedBrowser)
    {
    }

    public void AddCookie(string cookieName, string cookieValue, string path = "/")
    {
        Cookie cookie = new() { Name = cookieName, Value = cookieValue , Path = path};

        AddCookie(cookie);
    }

    public void AddCookie(System.Net.Cookie cookieToAdd)
    {
        var config = ServicesCollection.Current.Resolve<MapperConfiguration>();
        var mapper = config.CreateMapper();
        Cookie cookie = mapper.Map<Cookie>(cookieToAdd);

        AddCookie(cookie);
    }

    public void AddCookie(Cookie cookieToAdd)
    {
        CurrentContext.AddCookies(new Cookie[] { cookieToAdd });
    }

    public void DeleteAllCookies() => CurrentContext.ClearCookies();

    public void DeleteCookie(string cookieName)
    {
        var cookies = GetAllCookies().ToList();
        var cookieToRemove = cookies.First(x => x.Name.Equals(cookieName));
        cookies.Remove(cookieToRemove);

        DeleteAllCookies();

        Cookie[] updatedCookies = new Cookie[cookies.Count];

        var config = ServicesCollection.Current.Resolve<MapperConfiguration>();
        var mapper = config.CreateMapper();

        for (int i = 0; i < updatedCookies.Length; i++)
        {
            updatedCookies[i] = mapper.Map<Cookie>(cookies.ElementAt(i));
        }


        CurrentContext.AddCookies(updatedCookies);
    }

    public IReadOnlyList<BrowserContextCookiesResult> GetAllCookies()
    {
        return CurrentContext.Cookies();
    }

    public string GetCookie(string cookieName)
    {
        var cookies = GetAllCookies();
        return cookies.First(x => x.Name.Equals(cookieName)).Value;
    }
}