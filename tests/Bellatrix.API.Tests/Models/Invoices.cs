// <copyright file="Invoices.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace MediaStore.Demo.API.Models;

public class Invoices
{
    public Invoices() => InvoiceItems = new HashSet<InvoiceItems>();

    public long InvoiceId { get; set; }
    public long CustomerId { get; set; }
    public string InvoiceDate { get; set; }
    public string BillingAddress { get; set; }
    public string BillingCity { get; set; }
    public string BillingState { get; set; }
    public string BillingCountry { get; set; }
    public string BillingPostalCode { get; set; }
    public string Total { get; set; }

    public Customers Customer { get; set; }
    public ICollection<InvoiceItems> InvoiceItems { get; set; }
}
