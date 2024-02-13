// <copyright file="BddLoggingExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;
using Microsoft.Extensions.Configuration;

namespace Bellatrix.Web.Extensions;

public static class BddLoggingExtensions
{
    public static string AddUrlOrPageToBddLogging(this string loggingMessage, string location)
    {
        var loggingSettings = ConfigurationService.GetSection<WebSettings>();

        if (loggingSettings == null || string.IsNullOrEmpty(location))
        {
            return loggingMessage;
        }

        string result = loggingSettings.AddUrlToBddLogging ? $"{loggingMessage} on {location}" : loggingMessage;
        return result;
    }

    public static string AddDynamicTestCasesUsingLocatorsMessage(this string loggingMessage, ComponentActionEventArgs arg)
    {
        return $"{loggingMessage} {arg.Element.ComponentType.Name} using {arg.Element.LocatorType.Name} locator: '{arg.Element.LocatorValue}'";
    }
}
