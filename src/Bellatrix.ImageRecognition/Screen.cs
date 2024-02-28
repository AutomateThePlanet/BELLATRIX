// <copyright file="Screen.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using Bellatrix.ImageRecognition.Configuration;
using Bellatrix.ImageRecognition.Interfaces;
using Bellatrix.ImageRecognition.Models;
using Bellatrix.ImageRecognition.Utilities;

namespace Bellatrix.ImageRecognition;

public class Screen : IScreen
{
    private static readonly Regex InvalidTextRegex = new Regex(@"[\r\n\t\x00-\x1F]", RegexOptions.Compiled);
    private double _defaultSimilarity = 0.7;
    private double _timeoutInSeconds;
    private IImageRecognitionRuntime _runtime;

    public Screen()
    {
    }

    public Screen(IImageRecognitionRuntime imageRecognitionRuntime)
    {
        _runtime = imageRecognitionRuntime;
        _runtime.Start();
    }

    public bool Exists(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("exists", timeoutInSeconds), timeoutInSeconds);
        return true;
    }

    public void Click(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("click", timeoutInSeconds), timeoutInSeconds);
    }

    public void Click(string imagePath, Point offset, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(new OffsetImage(image, offset).ToSikuliScript("click", timeoutInSeconds), timeoutInSeconds);
    }

    public void DoubleClick(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("doubleClick", timeoutInSeconds), timeoutInSeconds);
    }

    public void DoubleClick(string imagePath, Point offset, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(new OffsetImage(image, offset).ToSikuliScript("doubleClick", timeoutInSeconds), timeoutInSeconds);
    }

    public void ValidateIsVisible(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("wait", timeoutInSeconds), timeoutInSeconds);
    }

    public void ValidateIsNotVisible(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("waitVanish", timeoutInSeconds), timeoutInSeconds);
    }

    public void SetText(string imagePath, string text, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        Click(imagePath, similarity, timeoutInSeconds);
        if (InvalidTextRegex.IsMatch(text))
        {
            throw new ArgumentException("Text cannot contain control characters");
        }

        var script = $"print \"SIKULI#: YES\" if type(\"{text}\") == 1 else \"SIKULI#: NO\"";
        _runtime.Run(script, timeoutInSeconds);
    }

    public void Hover(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("hover", timeoutInSeconds), timeoutInSeconds);
    }

    public void Hover(string imagePath, Point offset, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(new OffsetImage(image, offset).ToSikuliScript("hover", timeoutInSeconds), timeoutInSeconds);
    }

    public void RightClick(string imagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(image.ToSikuliScript("rightClick", timeoutInSeconds), timeoutInSeconds);
    }

    public void RightClick(string imagePath, Point offset, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage image = ImageFactory.FromFile(imagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        ValidateIsVisible(imagePath, similarity, timeoutInSeconds);
        _runtime.Run(new OffsetImage(image, offset).ToSikuliScript("rightClick", timeoutInSeconds), timeoutInSeconds);
    }

    public void DragDrop(string fromImagePath, string toImagePath, double? similarity = null, double? timeoutInSeconds = null)
    {
        InitializeRuntime();
        similarity = InitializeSimilarity(similarity);
        IImage fromImage = ImageFactory.FromFile(fromImagePath, similarity);
        IImage toImage = ImageFactory.FromFile(toImagePath, similarity);
        timeoutInSeconds = InitializeTimeout(timeoutInSeconds);
        var script = $"print \"SIKULI#: YES\" if dragDrop({fromImage.GeneratePatternString()},{toImage.GeneratePatternString()}0.0000 else \"SIKULI#: NO\"";
        _runtime.Run(script, timeoutInSeconds);
    }

    public void Dispose()
    {
        if (_runtime != null)
        {
            GC.SuppressFinalize(this);
            _runtime.Stop();
        }
    }

    private void InitializeRuntime()
    {
        if (_runtime == null)
        {
            var manager = new ImageRecognitionService();
            _defaultSimilarity = ConfigurationService.GetSection<ImageRecognitionSettings>().DefaultSimilarity;
            _timeoutInSeconds = ConfigurationService.GetSection<ImageRecognitionSettings>().TimeoutInSeconds;
            _runtime = new ImageRecognitionRuntime(manager);
            _runtime.Start();
        }
    }

    private double? InitializeTimeout(double? timeoutInSeconds)
    {
        return timeoutInSeconds ?? _timeoutInSeconds;
    }

    private double? InitializeSimilarity(double? defaultSimilarity)
    {
        return defaultSimilarity ?? _defaultSimilarity;
    }
}
