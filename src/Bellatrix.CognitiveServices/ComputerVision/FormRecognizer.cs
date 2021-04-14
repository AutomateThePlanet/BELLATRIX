// <copyright file="VisualService.cs" company="Automate The Planet Ltd.">
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
// <author>Ventsislav Ivanov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Bellatrix.CognitiveServices
{
    public class FormRecognizer
    {
        private readonly FormRecognizerClient _client;

        public FormRecognizer()
        {
            var cognitiveServicesSettings = ConfigurationService.GetSection<CognitiveServicesSettings>();
            string endpoint = cognitiveServicesSettings.ComputerVisionEndpoint;
            string subscriptionKey = cognitiveServicesSettings.ComputerVisionSubscriptionKey;

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(subscriptionKey))
            {
                throw new ArgumentException("To use the Computer Vision you need to set a valid endpoint and subscription key for Azure Computer Vision Cognitive Service in the testFrameworkSettings.json under imageRecognitionSettings section. Please check BELLATRIX docs for more info - https://docs.bellatrix.solutions/web-automation/image-recognition/");
            }

            _client = Authenticate(endpoint, subscriptionKey);
        }

        public FormPageCollection ValidateLayout(string expectedLayoutFilePath, string fileToBeValidatedPath)
        {
            var expectedAnalyzedFile = AnalyzePageFromLocalFile(expectedLayoutFilePath);
            var actualAnalyzedFile = AnalyzePageFromLocalFile(fileToBeValidatedPath);

            for (int i = 0; i < expectedAnalyzedFile.Count; i++)
            {
                Assert.AreEqual(expectedAnalyzedFile[i].Lines.Count, actualAnalyzedFile[i].Lines.Count, "The number of lines are different.");

                Console.WriteLine($"Form Page {expectedAnalyzedFile[i].PageNumber} has {expectedAnalyzedFile[i].Lines.Count} lines.");

                for (int l = 0; i < expectedAnalyzedFile[i].Lines.Count; l++)
                {
                    FormLine expectedLine = expectedAnalyzedFile[i].Lines[l];
                    Console.WriteLine($"  Expected Line {l} has {expectedLine.Words.Count} {(expectedLine.Words.Count == 1 ? "word" : "words")}, and text: '{expectedLine.Text}'.");
                    Console.WriteLine("    Its bounding box is:");
                    Console.WriteLine($"    Upper left => X: {expectedLine.BoundingBox[0].X}, Y= {expectedLine.BoundingBox[0].Y}");
                    Console.WriteLine($"    Upper right => X: {expectedLine.BoundingBox[1].X}, Y= {expectedLine.BoundingBox[1].Y}");
                    Console.WriteLine($"    Lower right => X: {expectedLine.BoundingBox[2].X}, Y= {expectedLine.BoundingBox[2].Y}");
                    Console.WriteLine($"    Lower left => X: {expectedLine.BoundingBox[3].X}, Y= {expectedLine.BoundingBox[3].Y}");

                    FormLine actualLine = actualAnalyzedFile[i].Lines[l];
                    Console.WriteLine($"  Expected Line {l} has {actualLine.Words.Count} {(actualLine.Words.Count == 1 ? "word" : "words")}, and text: '{actualLine.Text}'.");
                    Console.WriteLine("    Its bounding box is:");
                    Console.WriteLine($"    Upper left => X: {actualLine.BoundingBox[0].X}, Y= {actualLine.BoundingBox[0].Y}");
                    Console.WriteLine($"    Upper right => X: {actualLine.BoundingBox[1].X}, Y= {actualLine.BoundingBox[1].Y}");
                    Console.WriteLine($"    Lower right => X: {actualLine.BoundingBox[2].X}, Y= {actualLine.BoundingBox[2].Y}");
                    Console.WriteLine($"    Lower left => X: {actualLine.BoundingBox[3].X}, Y= {actualLine.BoundingBox[3].Y}");

                    Assert.AreEqual(expectedLine.BoundingBox[0].X, actualLine.BoundingBox[0].X, 0.1);
                    Assert.AreEqual(expectedLine.BoundingBox[0].Y, actualLine.BoundingBox[0].Y, 0.1);

                    Assert.AreEqual(expectedLine.BoundingBox[1].X, actualLine.BoundingBox[1].X, 0.1);
                    Assert.AreEqual(expectedLine.BoundingBox[1].Y, actualLine.BoundingBox[1].Y, 0.1);

                    Assert.AreEqual(expectedLine.BoundingBox[2].X, actualLine.BoundingBox[2].X, 0.1);
                    Assert.AreEqual(expectedLine.BoundingBox[2].Y, actualLine.BoundingBox[2].Y, 0.1);

                    Assert.AreEqual(expectedLine.BoundingBox[3].X, actualLine.BoundingBox[3].X, 0.1);
                    Assert.AreEqual(expectedLine.BoundingBox[3].Y, actualLine.BoundingBox[3].Y, 0.1);
                }
            }

            return actualAnalyzedFile;
        }

        public FormPageCollection AnalyzePageFromLocalFile(string localFile)
        {
            using var stream = new FileStream(localFile, FileMode.Open);

            Response<FormPageCollection> response = _client.StartRecognizeContentAsync(stream).WaitForCompletionAsync().Result;
            FormPageCollection formPages = response.Value;

            return formPages;
        }

        private FormRecognizerClient Authenticate(string endpoint, string key)
        {
            var credential = new AzureKeyCredential(key);
            var client = new FormRecognizerClient(new Uri(endpoint), credential);
            return client;
        }
    }
}
