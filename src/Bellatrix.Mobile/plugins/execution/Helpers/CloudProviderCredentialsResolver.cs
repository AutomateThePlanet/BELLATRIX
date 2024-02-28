// <copyright file="CloudProviderCredentialsResolver.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.KeyVault;

namespace Bellatrix.Mobile.Plugins;

public static class CloudProviderCredentialsResolver
{
    private const string USER_ENVIRONMENTAL_VARIABLE = "cloud.grid.user";
    private const string ACCESS_KEY_ENVIRONMENTAL_VARIABLE = "cloud.grid.key";

    public static Tuple<string, string> GetCredentials()
    {
        var user = SecretsResolver.GetSecret(USER_ENVIRONMENTAL_VARIABLE);
        var accessKey = SecretsResolver.GetSecret(ACCESS_KEY_ENVIRONMENTAL_VARIABLE);

        if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(accessKey))
        {
            return Tuple.Create(user, accessKey);
        }

        return GetCredentialsFromConfig();
    }

    private static Tuple<string, string> GetCredentialsFromConfig()
    {
        if (!ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.Arguments[0].ContainsKey(USER_ENVIRONMENTAL_VARIABLE) ||
            !ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.Arguments[0].ContainsKey(ACCESS_KEY_ENVIRONMENTAL_VARIABLE))
        {
            throw new ArgumentException($"To use grid execution you need to set environment variables called ({USER_ENVIRONMENTAL_VARIABLE} and {ACCESS_KEY_ENVIRONMENTAL_VARIABLE}) or set them in browser settings file.");
        }

        string user = ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.Arguments[0][USER_ENVIRONMENTAL_VARIABLE];
        string accessKey = ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.Arguments[0][ACCESS_KEY_ENVIRONMENTAL_VARIABLE];

        return Tuple.Create(user, accessKey);
    }
}
