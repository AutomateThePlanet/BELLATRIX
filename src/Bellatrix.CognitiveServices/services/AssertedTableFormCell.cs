// <copyright file="AssertedTableFormCell.cs" company="Automate The Planet Ltd.">
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

public class AssertedTableFormCell
{
    public static event EventHandler<string> TableFormCellAsserted;
    private readonly FormTableCell _formTableCell;

    public AssertedTableFormCell(FormTableCell formTableCell)
    {
        _formTableCell = formTableCell;
    }

    public int Row => _formTableCell.RowIndex;
    public int Column => _formTableCell.ColumnIndex;
    public string Text => _formTableCell.Text;
    public FieldBoundingBox BoundingBox => _formTableCell.BoundingBox;
    public FormTableCell WrappedFormTableCell => _formTableCell;

    public void AssertTextEquals(string expectedText)
    {
        TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text = {expectedText}");
        Assert.AreEqual(expectedText, Text, $"Cell[{Row},{Column}] Text != {expectedText}");
    }

    public void AssertTextContains(string expectedText)
    {
        TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text contains {expectedText}");
        Assert.IsTrue(Text.Contains(expectedText), $"Cell[{Row},{Column}] Text didn't contain {expectedText}");
    }

    public void AssertTextNotContains(string expectedText)
    {
        TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text doesn't contain {expectedText}");
        Assert.IsFalse(Text.Contains(expectedText), $"Cell[{Row},{Column}] Text contained {expectedText}");
    }
}
