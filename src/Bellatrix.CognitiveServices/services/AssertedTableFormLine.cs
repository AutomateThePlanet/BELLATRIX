// <copyright file="AssertedTableFormLine.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.Assertions;

namespace Bellatrix.CognitiveServices.services;

public class AssertedTableFormLine
{
    public static event EventHandler<string> TableFormLineAsserted;
    private readonly FormLine _formLine;

    public AssertedTableFormLine(FormLine formLine)
    {
        _formLine = formLine;
    }

    public List<string> Words => _formLine.Words.Select(x => x.Text).ToList();
    public FieldBoundingBox BoundingBox => _formLine.BoundingBox;
    public FormLine WrappedFormLine => _formLine;

    public void AssertWordsCount(int expectedWordsCount)
    {
        TableFormLineAsserted?.Invoke(this, $"Assert line words' count = {expectedWordsCount}");
        Assert.AreEqual(expectedWordsCount, _formLine.Words.Count, $"Line words' count != {expectedWordsCount}");
    }

    public void AssertWordsEqual(params string[] expectedWords)
    {
        TableFormLineAsserted?.Invoke(this, $"Assert line words are {expectedWords}");
        CollectionAssert.AreEqual(expectedWords, Words, "Expected words are different than the actual ones present on the line.");
    }

    public void AssertWordsContain(params string[] expectedWords)
    {
        TableFormLineAsserted?.Invoke(this, $"Assert line words contain the words: {expectedWords}");
        CollectionAssert.IsSubsetOf(expectedWords, Words, "Expected words are different than the actual ones present on the line.");
    }
}
