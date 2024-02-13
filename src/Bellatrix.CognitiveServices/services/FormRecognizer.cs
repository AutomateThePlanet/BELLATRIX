// <copyright file="FormRecognizer.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.Assertions;
using Bellatrix.CognitiveServices.services;
using Bellatrix.Infrastructure;
using Bellatrix.KeyVault;
namespace Bellatrix.CognitiveServices;

public class FormRecognizer
{
    // TODO: Anton(11.05.2021): Add additional methods similar to Web.Table for selecting rows, columns and cells + their validation.
    private readonly FormRecognizerClient _client;

    public FormRecognizer()
    {
        string endpoint = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<CognitiveServicesSettings>().FormRecognizerEndpoint);
        string subscriptionKey = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<CognitiveServicesSettings>().FormRecognizerSubscriptionKey);

        if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(subscriptionKey))
        {
            throw new ArgumentException("To use the FormRecognizer you need to set a valid endpoint and subscription key for Azure Form Recognizer Cognitive Service in the testFrameworkSettings.json under cognitiveServicesSettings section. Please check BELLATRIX docs for more info - https://docs.bellatrix.solutions/web-automation/image-recognition/");
        }

        _client = Authenticate(endpoint, subscriptionKey);
    }

    public FormPageCollection ValidateFormLayout(string expectedLayoutFilePath, string fileToBeValidatedPath, double delta = 0.1)
    {
        var expectedAnalyzedFile = AnalyzeFile(expectedLayoutFilePath);
        var actualAnalyzedFile = AnalyzeFile(fileToBeValidatedPath);

        for (int i = 0; i < expectedAnalyzedFile.Count; i++)
        {
            Assert.AreEqual(expectedAnalyzedFile[i].Lines.Count, actualAnalyzedFile[i].Lines.Count, "The number of lines are different.");

            Logger.LogInformation($"Form Page {expectedAnalyzedFile[i].PageNumber} has {expectedAnalyzedFile[i].Lines.Count} lines.");

            for (int l = 0; i < expectedAnalyzedFile[i].Lines.Count; l++)
            {
                FormLine expectedLine = expectedAnalyzedFile[i].Lines[l];
                Logger.LogInformation($"  Expected Line {l} has {expectedLine.Words.Count} {(expectedLine.Words.Count == 1 ? "word" : "words")}, and text: '{expectedLine.Text}'.");
                Logger.LogInformation("    Its bounding box is:");
                Logger.LogInformation($"    Upper left => X: {expectedLine.BoundingBox[0].X}, Y= {expectedLine.BoundingBox[0].Y}");
                Logger.LogInformation($"    Upper right => X: {expectedLine.BoundingBox[1].X}, Y= {expectedLine.BoundingBox[1].Y}");
                Logger.LogInformation($"    Lower right => X: {expectedLine.BoundingBox[2].X}, Y= {expectedLine.BoundingBox[2].Y}");
                Logger.LogInformation($"    Lower left => X: {expectedLine.BoundingBox[3].X}, Y= {expectedLine.BoundingBox[3].Y}");

                FormLine actualLine = actualAnalyzedFile[i].Lines[l];
                Logger.LogInformation($"  Expected Line {l} has {actualLine.Words.Count} {(actualLine.Words.Count == 1 ? "word" : "words")}, and text: '{actualLine.Text}'.");
                Logger.LogInformation("    Its bounding box is:");
                Logger.LogInformation($"    Upper left => X: {actualLine.BoundingBox[0].X}, Y= {actualLine.BoundingBox[0].Y}");
                Logger.LogInformation($"    Upper right => X: {actualLine.BoundingBox[1].X}, Y= {actualLine.BoundingBox[1].Y}");
                Logger.LogInformation($"    Lower right => X: {actualLine.BoundingBox[2].X}, Y= {actualLine.BoundingBox[2].Y}");
                Logger.LogInformation($"    Lower left => X: {actualLine.BoundingBox[3].X}, Y= {actualLine.BoundingBox[3].Y}");

                Assert.AreEqual(expectedLine.BoundingBox[0].X, actualLine.BoundingBox[0].X, delta);
                Assert.AreEqual(expectedLine.BoundingBox[0].Y, actualLine.BoundingBox[0].Y, delta);

                Assert.AreEqual(expectedLine.BoundingBox[1].X, actualLine.BoundingBox[1].X, delta);
                Assert.AreEqual(expectedLine.BoundingBox[1].Y, actualLine.BoundingBox[1].Y, delta);

                Assert.AreEqual(expectedLine.BoundingBox[2].X, actualLine.BoundingBox[2].X, delta);
                Assert.AreEqual(expectedLine.BoundingBox[2].Y, actualLine.BoundingBox[2].Y, delta);

                Assert.AreEqual(expectedLine.BoundingBox[3].X, actualLine.BoundingBox[3].X, delta);
                Assert.AreEqual(expectedLine.BoundingBox[3].Y, actualLine.BoundingBox[3].Y, delta);
            }
        }

        return actualAnalyzedFile;
    }

    public AssertedFormPage Analyze(string localFileToBeAnalyzed, int pageNumber = 0)
    {
        return new AssertedFormPage(AnalyzeFile(localFileToBeAnalyzed), pageNumber);
    }

    public FormPageCollection AnalyzeFile(string localFileToBeAnalyzed)
    {
        using var stream = new FileStream(localFileToBeAnalyzed, FileMode.Open);

        Response<FormPageCollection> response = _client.StartRecognizeContentAsync(stream).WaitForCompletionAsync().Result;
        FormPageCollection formPages = response.Value;

        return formPages;
    }

    public string SaveAnalyzedFileToJson(string localFileToBeAnalyzed, string outputFilePath)
    {
        using var stream = new FileStream(localFileToBeAnalyzed, FileMode.Open);

        Response<FormPageCollection> response = _client.StartRecognizeContentAsync(stream).WaitForCompletionAsync().Result;
        FormPageCollection formPages = response.Value;
        var jsonSerializer = new Infrastructure.JsonSerializer();

        string jsonContent = jsonSerializer.Serialize(formPages);
        new FileFacade().WriteAllText(outputFilePath, jsonContent);

        return jsonContent;
    }

    private FormRecognizerClient Authenticate(string endpoint, string key)
    {
        var credential = new AzureKeyCredential(key);
        var client = new FormRecognizerClient(new Uri(endpoint), credential);
        return client;
    }
}