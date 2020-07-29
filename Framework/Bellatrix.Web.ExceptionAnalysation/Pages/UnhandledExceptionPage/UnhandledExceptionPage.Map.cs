// <copyright file="UnhandledExceptionPage.Map.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Controls;

namespace Bellatrix.Web.ExceptionAnalysation.Pages
{
    public partial class UnhandledExceptionPage
    {
        public Container ExceptionMessage => Element.CreateByXpath<Container>("/html/body/span[1]/h2/i");

        public Container ExceptionStackTrace => Element.CreateByXpath<Container>("/html/body/font/table[2]/tbody/tr/td/code/pre");
    }
}