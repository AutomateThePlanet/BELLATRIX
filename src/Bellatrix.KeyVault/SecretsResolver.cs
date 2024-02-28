// <copyright file="SecretsResolver.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.KeyVault;

public static class SecretsResolver
{
    public static string GetSecret(Func<string> getConfigValue)
    {
        if (getConfigValue().StartsWith("env_"))
        {
            string environmentalVariable = Environment.GetEnvironmentVariable(getConfigValue().Replace("env_", string.Empty), EnvironmentVariableTarget.Machine);
            return environmentalVariable;
        }
        else if (getConfigValue().StartsWith("vault_"))
        {
            string keyVaultValue = KeyVault.GetSecret(getConfigValue().Replace("vault_", string.Empty));
            return keyVaultValue;
        }
        else
        {
            return getConfigValue();
        }
    }

    public static string GetSecret(string name)
    {
        string environmentalVariable = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        if (!string.IsNullOrEmpty(environmentalVariable))
        {
            return environmentalVariable;
        }
        else if (KeyVault.IsAvailable)
        {
            string keyVaultValue = KeyVault.GetSecret(name);
            return keyVaultValue;
        }
        else
        {
            throw new ArgumentException("You need to initialize an environmental variable or key vault secret first.");
        }
    }
}