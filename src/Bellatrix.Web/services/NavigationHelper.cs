// <copyright file="NavigationHelper.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Helpers;

namespace Bellatrix.Web;

internal class NavigationHelper : INavigationHelper
{
    public string GetHostByLocation(string currentLocation)
    {
        var currentHost = string.Empty;
        return currentHost;
    }

    public string FormatUrl(string relativeUrl, string host, bool sslEnabled)
    {
        var url = relativeUrl;
        if (!relativeUrl.StartsWith("/", StringComparison.Ordinal) &&
            relativeUrl.StartsWith(@"~/", StringComparison.Ordinal))
        {
            url = relativeUrl.Substring(2);
        }

        url = string.Concat(host, url);

        if (sslEnabled)
        {
            url = url.Replace("http://", "https://");
        }

        return url;
    }

    public string GetAbsoluteUrl(string relativeUrl, string currentLocation, bool sslEnabled = false)
    {
        var host = GetHostByLocation(currentLocation);
        return FormatUrl(relativeUrl, host, sslEnabled);
    }
}