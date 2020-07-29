// <copyright file="ConfigurationServiceExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Configuration;
using Bellatrix.Web.Proxy;
using Microsoft.Extensions.Configuration;

namespace Bellatrix
{
    public static class ConfigurationServiceExtensions
    {
        public static WebProxySettings GetWebProxySettings(this ConfigurationService service)
        {
            var result = ConfigurationService.Instance.Root.GetSection("webProxySettings").Get<WebProxySettings>();

            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(WebProxySettings).ToString());
            }

            return result;
        }
    }
}