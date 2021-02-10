// <copyright file="CloudProviderCredentialsResolver.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix.Mobile.Plugins
{
    public abstract class CloudProviderCredentialsResolver
    {
        public Tuple<string, string> GetCredentials()
        {
            var user = Environment.GetEnvironmentVariable(UserEnvironmentVariable);
            var accessKey = Environment.GetEnvironmentVariable(AccessKeyEnvironmentVariable);

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(accessKey))
            {
                return Tuple.Create(user, accessKey);
            }

            return GetCredentialsFromConfig();
        }

        protected abstract string UserEnvironmentVariable { get; }
        protected abstract string AccessKeyEnvironmentVariable { get; }
        protected abstract Tuple<string, string> GetCredentialsFromConfig();
    }
}
