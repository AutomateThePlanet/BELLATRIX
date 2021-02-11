// <copyright file="BrowserStackCredentialsResolver.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Configuration;

namespace Bellatrix.Mobile.Plugins
{
    public class BrowserStackCredentialsResolver : CloudProviderCredentialsResolver
    {
        protected override string UserEnvironmentVariable => "browserstack.user";
        protected override string AccessKeyEnvironmentVariable => "browserstack.accessKey";

        protected override Tuple<string, string> GetCredentialsFromConfig()
        {
            string user = ConfigurationService.GetSection<MobileSettings>().BrowserStack.User;
            string accessKey = ConfigurationService.GetSection<MobileSettings>().BrowserStack.Key;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(accessKey))
            {
                throw new ArgumentException("To use BrowserStack execution you need to set environment variables called (browserstack.user and browserstack.accessKey) or set them in browser settings file.");
            }

            return Tuple.Create(user, accessKey);
        }
    }
}
