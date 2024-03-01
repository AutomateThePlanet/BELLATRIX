// <copyright file="Fields.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.BugReporting.Jira;

namespace Bellatrix.BugReporting.Jira.Model;

public class Fields
{
    public string summary { get; set; }

    public Priority priority { get; set; }

    public Status status { get; set; }

    public DateTime created { get; set; }

    public List<string> labels { get; set; }

    public List<Attachment> attachment { get; set; }

    public Description description { get; set; }

    ////[JsonProperty("customfield_10090")]
    ////public CustomField RootCauseType { get; set; }

    ////[JsonProperty("customfield_10091")]
    ////public CustomField Severity { get; set; }

    ////[JsonProperty("customfield_10092")]
    ////public CustomField RejectionReason { get; set; }

    ////[JsonProperty("customfield_10085")]
    ////public CustomField RootCauseAnalysis { get; set; }

    ////[JsonProperty("customfield_10086")]
    ////public CustomField BugOrigin { get; set; }

    ////[JsonProperty("customfield_10087")]
    ////public CustomField FunctionalArea { get; set; }

    ////[JsonProperty("customfield_10088")]
    ////public CustomField RootCauseReason { get; set; }

    ////[JsonProperty("customfield_10089")]
    ////public CustomField BugAppearancePhase { get; set; }
}
