// <copyright file="TableHeaderRow.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Diagnostics;
using Bellatrix.Web.Contracts;

namespace Bellatrix.Web
{
    public class TableHeaderRow : Element, IElementInnerHtml
    {
        public static Func<TableHeaderRow, string> OverrideInnerHtmlGlobally;
        public static Func<TableHeaderRow, string> OverrideInnerHtmlLocally;

        public ElementsList<TableCell> HeaderCells => this.CreateAllByTag<TableCell>("th", true);

        public static new void ClearLocalOverrides()
        {
            OverrideInnerHtmlLocally = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerHtml
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerHtmlGlobally, OverrideInnerHtmlLocally, DefaultInnerHtml);
                return action();
            }
        }

        protected virtual string DefaultInnerHtml(TableCell tableCell) => base.DefaultInnerHtml(tableCell);
    }
}