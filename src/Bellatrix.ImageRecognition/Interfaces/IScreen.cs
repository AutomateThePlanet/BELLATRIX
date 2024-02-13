// <copyright file="IScreen.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;

namespace Bellatrix.ImageRecognition.Interfaces;

public interface IScreen : IDisposable
{
    void Click(string imagePath, double? similarity, double? timeoutInSeconds);
    void Click(string imagePath, Point offset, double? similarity, double? timeoutInSeconds);
    void DoubleClick(string imagePath, double? similarity, double? timeoutInSeconds);
    void DoubleClick(string imagePath, Point offset, double? similarity, double? timeoutInSeconds);
    void RightClick(string imagePath, double? similarity, double? timeoutInSeconds);
    void RightClick(string imagePath, Point offset, double? similarity, double? timeoutInSeconds);
    void SetText(string imagePath, string text, double? similarity, double? timeoutInSeconds);
    void Hover(string imagePath, double? similarity, double? timeoutInSeconds);
    void Hover(string imagePath, Point offset, double? similarity, double? timeoutInSeconds);
    void DragDrop(string fromImagePath, string toImagePath, double? similarity, double? timeoutInSeconds);
    bool Exists(string imagePath, double? similarity, double? timeoutInSeconds);
    void ValidateIsVisible(string imagePath, double? similarity, double? timeoutInSeconds);
    void ValidateIsNotVisible(string imagePath, double? similarity, double? timeoutInSeconds);
}