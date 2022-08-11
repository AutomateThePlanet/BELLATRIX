// <copyright file="SecretsResolver.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.AWS;

public static class SecretsResolver
{
    public static string GetSecret(RegionEndpoint region, Func<string> getConfigValue)
    {
        if (getConfigValue().StartsWith("env_"))
        {
            string environmentalVariable = Environment.GetEnvironmentVariable(getConfigValue().Replace("env_", string.Empty), EnvironmentVariableTarget.Machine);
            return environmentalVariable;
        }
        else if (getConfigValue().StartsWith("smaws_"))
        {
            string keyVaultValue = SecretsManager.GetSecret(region, getConfigValue().Replace("smaws_", string.Empty));
            return keyVaultValue;
        }
        else
        {
            return getConfigValue();
        }
    }
}