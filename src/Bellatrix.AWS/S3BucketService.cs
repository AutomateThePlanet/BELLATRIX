// <copyright file="S3BucketService.cs" company="Automate The Planet Ltd.">
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
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Collections.Generic;
using System.Threading;

namespace Bellatrix.AWS;
public class S3BucketService
{
    public void DownloadFile(RegionEndpoint region, string bucketName, string key, string resultFilePath)
    {
        using AmazonS3Client client = InitializeS3Client(region);
        var request = new GetObjectRequest();
        request.BucketName = bucketName;
        request.Key = key;
        GetObjectResponse response = client.GetObjectAsync(request).Result;
        response.WriteResponseStreamToFileAsync(resultFilePath, false, new CancellationToken()).Wait();
    }

    public void UploadFile(RegionEndpoint region, string bucketName, string sourceFilePath, string resultFileName)
    {
        using AmazonS3Client client = InitializeS3Client(region);
        TransferUtility utility = new TransferUtility(client);
        TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
        request.BucketName = bucketName;
        request.Key = resultFileName;
        request.FilePath = sourceFilePath;
        utility.Upload(request);
    }

    public void DeleteFile(RegionEndpoint region, string bucketName, string key)
    {
        using AmazonS3Client client = InitializeS3Client(region);
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };

        client.DeleteObjectAsync(deleteObjectRequest).Wait();
    }

    public void DeleteMultipleFiles(RegionEndpoint region, string bucketName, List<KeyVersion> filesToBeDeleted)
    {
        using AmazonS3Client client = InitializeS3Client(region);
        DeleteObjectsRequest multiObjectDeleteRequest = new DeleteObjectsRequest
        {
            BucketName = bucketName,
            Objects = filesToBeDeleted
        };
        client.DeleteObjectsAsync(multiObjectDeleteRequest).Wait();
    }
     
    private AmazonS3Client InitializeS3Client(RegionEndpoint region)
    {
        AmazonS3Config config = new AmazonS3Config();
        config.RegionEndpoint = region;
        var s3Client = new AmazonS3Client(config);
        return s3Client;
    }
}
