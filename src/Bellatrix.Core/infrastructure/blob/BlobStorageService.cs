// <copyright file="BlobStorageService.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bellatrix;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Bellatrix.Core.Infrastructure;

public class BlobStorageService
{
    private string _connectionString;

    public BlobStorageService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public BlobStorageService()
    {
        _connectionString = ConfigurationService.GetSection<BlobStorageSettings>().ConnectionString;
    }

    public void DownloadFile(string fileName, string downloadFilePath, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        try
        {
            pageBlob.DownloadToFileAsync(downloadFilePath, FileMode.CreateNew).Wait();
        }
        catch (Exception ex)
        {
            DebugInformation.PrintStackTrace(ex);
        }
    }

    public string GetFileUrl(string fileName, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        return pageBlob.Uri.AbsoluteUri;
    }

    public bool CheckIfFileExists(string fileName, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        return pageBlob.ExistsAsync().Result;
    }

    public string UploadFile(string fileName, string filePath, string blobContainerName, string contentType)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlockBlobReference(fileName);
        pageBlob.Properties.ContentType = contentType;
        pageBlob.UploadFromFileAsync(filePath).Wait();

        return pageBlob.Uri.AbsoluteUri;
    }
}
