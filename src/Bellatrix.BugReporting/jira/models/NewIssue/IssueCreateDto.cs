// <copyright file="IssueCreateDto.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.BugReporting.Jira;

public class IssueCreateDto
{
    public static IssueCreateDto CreateDto(string projectName, string priority, string title, List<string> descriptionLines)
    {
        var issueCreateDto = new IssueCreateDto();

        issueCreateDto.fields = new Fields();
        issueCreateDto.fields.project = new Project()
        {
            key = projectName,
        };

        issueCreateDto.fields.issuetype = new Issuetype()
        {
            name = "Bug",
        };

        issueCreateDto.fields.summary = title;
        issueCreateDto.fields.priority = new Priority()
        {
            name = priority,
        };

        issueCreateDto.fields.description = new Description()
        {
            version = 1,
            type = "doc",
            content = new List<Content>()
            {
                new Content()
                {
                    type = "paragraph",
                    content = new List<Content2>(),
                },
            },
        };

        foreach (var currentLine in descriptionLines)
        {
            issueCreateDto.fields.description.content.First().content.Add(new Content2() { type = "text", text = currentLine });
            issueCreateDto.fields.description.content.First().content.Add(new Content2() { type = "text", text = Environment.NewLine });
        }

        return issueCreateDto;
    }

    public Fields fields { get; set; }

    public class Project
    {
        public string key { get; set; }
    }

    public class Content2
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public List<Content2> content { get; set; }
    }

    public class Description
    {
        public int version { get; set; }
        public string type { get; set; }
        public List<Content> content { get; set; }
    }

    public class Issuetype
    {
        public string name { get; set; }
    }

    public class Priority
    {
        public string name { get; set; }
    }

    public class Fields
    {
        public Project project { get; set; }
        public string summary { get; set; }
        public Description description { get; set; }
        public Issuetype issuetype { get; set; }
        public Priority priority { get; set; }
    }
}
