﻿// <copyright file="AnalyzedTestException.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

namespace Bellatrix.ExceptionAnalysation;

[Serializable]
public class AnalyzedTestException : Exception
{
    public AnalyzedTestException()
    {
    }

    public AnalyzedTestException(string message)
        : base(FormatExceptionMessage(message))
    {
    }

    public AnalyzedTestException(string message, string url, Exception inner)
        : base(FormatExceptionMessage(message, url), inner)
    {
    }

    private static string FormatExceptionMessage(string exceptionMessage) => $"{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}{Environment.NewLine}{exceptionMessage}{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}";

    private static string FormatExceptionMessage(string exceptionMessage, string url) => $"{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine}{Environment.NewLine}{exceptionMessage}{Environment.NewLine}{Environment.NewLine}{new string('#', 40)}{Environment.NewLine} URL: {url}";
}