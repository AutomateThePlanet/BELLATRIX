// <copyright file="AssertedFormPage.cs" company="Automate The Planet Ltd.">
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

public class AssertedFormPage
{
    public static event EventHandler<string> FormPageAsserted;

    public FormPage FormPage { get; }

    public AssertedFormPage(FormPageCollection formPages, int pageNumber)
    {
        FormPage = formPages[pageNumber];
        Lines = FormPage.Lines.Select(x => new AssertedTableFormLine(x)).ToList();
    }

    public List<AssertedTableFormLine> Lines { get; set; }

    public AssertedFormTable GetTable(int index = 0)
    {
        return new AssertedFormTable(FormPage.Tables[index]);
    }

    public void AssertLinesCount(int expectedLinesCount)
    {
        FormPageAsserted?.Invoke(this, $"Assert page lines' count = {expectedLinesCount}");
        Assert.AreEqual(expectedLinesCount, FormPage.Lines.Count, $"Page lines' count != {expectedLinesCount}");
    }
}