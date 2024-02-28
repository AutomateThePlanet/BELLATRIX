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
// <author>Ventsislav Ivanov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.KeyVault;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Bellatrix.CognitiveServices;

public class ComputerVision
{
    private readonly ComputerVisionClient _client;

    public ComputerVision()
    {
        string endpoint = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<CognitiveServicesSettings>().ComputerVisionEndpoint);
        string subscriptionKey = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<CognitiveServicesSettings>().ComputerVisionSubscriptionKey);

        if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(subscriptionKey))
        {
            throw new ArgumentException("To use the ComputerVision you need to set a valid endpoint and subscription key for Azure Computer Vision Cognitive Service in the testFrameworkSettings.json under cognitiveServicesSettings section. Please check BELLATRIX docs for more info - https://docs.bellatrix.solutions/web-automation/image-recognition/");
        }

        _client = Authenticate(endpoint, subscriptionKey);
    }

    public void ValidateText(string localFile, string language, List<string> expectedTextSnippets)
    {
        var actualTextSnippets = ExtractOCRTextFromLocalFile(localFile, language);
        CollectionAssert.IsSubsetOf(expectedTextSnippets, actualTextSnippets, "Some of the expected text snippets weren't present on the actual PDF/Image.");
    }

    public List<string> ExtractOCRTextFromLocalFile(string localFile, string language = "en")
    {
        var textHeaders = _client.ReadInStreamAsync(File.OpenRead(localFile), language: language).Result;

        // After the request, get the operation location (operation ID)
        string operationLocation = textHeaders.OperationLocation;
        Thread.Sleep(2000);

        // Retrieve the URI where the recognized text will be stored from the Operation-Location header.
        // We only need the ID and not the full URL
        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        // Extract the text
        ReadOperationResult results;
        do
        {
            results = _client.GetReadResultAsync(Guid.Parse(operationId)).Result;
        }
        while (results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted);

        List<string> foundTextSnippets = new List<string>();
        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                foundTextSnippets.Add(line.Text);
            }
        }

        return foundTextSnippets;
    }

    private ComputerVisionClient Authenticate(string endpoint, string key)
    {
        ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        return client;
    }
}