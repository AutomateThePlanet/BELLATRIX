// <copyright file="InvoiceItems.cs" company="Automate The Planet Ltd.">
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
namespace MediaStore.Demo.API.Models;

public class InvoiceItems
{
    public long InvoiceLineId { get; set; }
    public long InvoiceId { get; set; }
    public long TrackId { get; set; }
    public string UnitPrice { get; set; }
    public long Quantity { get; set; }

    public Invoices Invoice { get; set; }
    public Tracks Track { get; set; }
}
