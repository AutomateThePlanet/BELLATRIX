// <copyright file="Email.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Email;

public class Email : IComparer<Email>
{
    public string cc { get; set; }
    public DateTime date { get; set; }
    public List<object> attachments { get; set; }
    public string dkim { get; set; }
    public string envelope_to { get; set; }
    public string subject { get; set; }
    public string SPF { get; set; }
    public string messageId { get; set; }
    public List<ToParsed> to_parsed { get; set; }
    public string oid { get; set; }
    public string envelope_from { get; set; }
    public List<object> cc_parsed { get; set; }
    public string @namespace { get; set; }
    public string from { get; set; }
    public string sender_ip { get; set; }
    public string html { get; set; }
    public string to { get; set; }
    public string tag { get; set; }
    public string text { get; set; }
    public List<FromParsed> from_parsed { get; set; }
    public object timestamp { get; set; }
    public string id { get; set; }
    public string downloadUrl { get; set; }

    public int Compare(Email x, Email y)
    {
        return x.date.CompareTo(y.date);
    }
}
