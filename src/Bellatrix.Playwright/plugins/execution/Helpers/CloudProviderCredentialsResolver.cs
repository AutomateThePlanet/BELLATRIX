// <copyright file="CloudProviderCredentialsResolver.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright.Plugins.Browser;

public static class CloudProviderCredentialsResolver
{
    public static event EventHandler<CapabilityValueResolvingEventArgs> CapabilityValueResolving;

    private const string USER_ENVIRONMENTAL_VARIABLE = "cloud.grid.user";
    private const string ACCESS_KEY_ENVIRONMENTAL_VARIABLE = "cloud.grid.key";

    public static Tuple<string, string> GetCredentials()
    {
        var user = Environment.GetEnvironmentVariable(USER_ENVIRONMENTAL_VARIABLE);
        var accessKey = Environment.GetEnvironmentVariable(ACCESS_KEY_ENVIRONMENTAL_VARIABLE);
        
        var resolvingUserArgs = new CapabilityValueResolvingEventArgs(USER_ENVIRONMENTAL_VARIABLE);
        CapabilityValueResolving?.Invoke(null, resolvingUserArgs);
        if (resolvingUserArgs.Handled) user = (string)resolvingUserArgs.ResolvedValue;
        
        var resolvingAccessKeyArgs = new CapabilityValueResolvingEventArgs(ACCESS_KEY_ENVIRONMENTAL_VARIABLE);
        CapabilityValueResolving?.Invoke(null, resolvingAccessKeyArgs);
        if (resolvingAccessKeyArgs.Handled) accessKey = (string)resolvingAccessKeyArgs.ResolvedValue;

        if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(accessKey))
        {
            return Tuple.Create(user, accessKey);
        }

        return GetCredentialsFromConfig();
    }

    private static Tuple<string, string> GetCredentialsFromConfig()
    {
        if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].CloudGridUser is null or "" ||
            ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].CloudGridKey is null or "")
        {
            throw new ArgumentException($"To use grid execution you need to set environment variables called ({USER_ENVIRONMENTAL_VARIABLE} and {ACCESS_KEY_ENVIRONMENTAL_VARIABLE}) or set them in browser settings file.");
        }

        string user = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].CloudGridUser;
        string accessKey = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].CloudGridKey;

        return Tuple.Create(user, accessKey);
    }
}