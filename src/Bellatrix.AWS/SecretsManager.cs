// <copyright file="SecretsManager.cs" company="Automate The Planet Ltd.">
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
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Bellatrix.AWS;

public class SecretsManager
{
    public string GetSecret(RegionEndpoint region, string name)
    {
        using var secretsManager = InitializeSecretsManagerClient(region);
        var request = new GetSecretValueRequest
        {
            SecretId = name
        };
        var secret = secretsManager.GetSecretValueAsync(request).Result;
        return secret.SecretString;
    }

    private AmazonSecretsManagerClient InitializeSecretsManagerClient(RegionEndpoint region)
    {
        var config = new AmazonSecretsManagerConfig();
        config.RegionEndpoint = region;
        var client = new AmazonSecretsManagerClient(config);
        return client;
    }
}