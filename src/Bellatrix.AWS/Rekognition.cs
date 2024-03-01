// <copyright file="ComputerVision.cs" company="Automate The Planet Ltd.">
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
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Bellatrix.Assertions;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.AWS;
public class Rekognition
{
    public List<string> ExtractOCRTextFromLocalFile(RegionEndpoint region, string bucket, string fileName)
    {
        List<string> textSnippets = ExtractBlocksFromLocalFile(region, bucket, fileName).Select(t => t.DetectedText).ToList();
        return textSnippets;
    }

    public void ValidateText(RegionEndpoint region, string bucket, string fileName, List<string> expectedTextSnippets)
    {
        var actualTextSnippets = ExtractBlocksFromLocalFile(region, bucket, fileName);

        CollectionAssert.AreEqual(actualTextSnippets, expectedTextSnippets, "Some of the expected text snippets weren't present on the actual PDF/Image.");
    }

    public List<TextDetection> ExtractBlocksFromLocalFile(RegionEndpoint region, string bucket, string fileName)
    {
        using var rekognitionClient = InitializeRekognitionClient(region);
        var detectTextRequest = new DetectTextRequest()
        {
            Image = new Image()
            {
                S3Object = new S3Object()
                {
                    Name = fileName,
                    Bucket = bucket,
                },
            },
        };
        var response = rekognitionClient.DetectTextAsync(detectTextRequest).Result;
        return response.TextDetections;
    }

    private AmazonRekognitionClient InitializeRekognitionClient(RegionEndpoint region)
    {
        var config = new AmazonRekognitionConfig();
        config.RegionEndpoint = region;
        var client = new AmazonRekognitionClient(config);
        return client;
    }
}