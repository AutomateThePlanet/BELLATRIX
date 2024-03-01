// <copyright file="KeyVault.cs" company="Automate The Planet Ltd.">
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
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Bellatrix.KeyVault;

public static class KeyVault
{
    private static SecretClient _secretClient;

    static KeyVault()
    {
        InitializeClient();
    }

    public static bool IsAvailable = _secretClient != null;

    public static string GetSecret(string name)
    {
        if (_secretClient == null)
        {
            return null;
        }

        var secret = _secretClient.GetSecret(name);
        return secret.Value.Value;
    }

    private static void InitializeClient()
    {
        if (_secretClient == null)
        {
            var settings = ConfigurationService.GetSection<KeyVaultSettings>();
            if (settings.IsEnabled && !string.IsNullOrEmpty(settings.KeyVaultEndpoint))
            {
                // Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
                // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
                var cred = new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential());
                _secretClient = new SecretClient(vaultUri: new Uri(settings.KeyVaultEndpoint), cred);
            }
        }
    }
}