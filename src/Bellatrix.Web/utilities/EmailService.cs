// <copyright file="EmailService.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using Bellatrix;
using Bellatrix.Core.Infrastructure;
using Bellatrix.Infrastructure;
using Bellatrix.Web;

namespace Bellatrix.Web.Utilities
{
    public static class EmailService
    {
        private static BlobStorageService _blobStorageService;

        static EmailService()
        {
            _blobStorageService = new BlobStorageService();
        }

        public static Email ReadEmail(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = ServicesCollection.Current.Resolve<CookiesService>().GetCookie("set_azure_emails");
            }

            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            _blobStorageService.DownloadFile(name, tempFilePath, "emails");
            string fileContent = new FileFacade().ReadAllText(tempFilePath);
            var email = new JsonSerializer().Deserialize<Email>(fileContent);
            return email;
        }

        public static string LoadEmailBody(string htmlBody)
        {
            htmlBody = htmlBody.Replace("\n", string.Empty).Replace("\\/", "/").Replace("\\\"", "\"");
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");
            File.WriteAllText(tempFilePath, htmlBody);
            var navigationService = ServicesCollection.Current.Resolve<NavigationService>();
            navigationService.NavigateToLocalPage(tempFilePath);
            return htmlBody;
        }
    }
}
