// <copyright file="DynamicTestCaseAttribute.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix
{
    [AttributeUsage(AttributeTargets.All)]
    public class QTestIdAttribute : Attribute
    {
        public string TestCaseId { get; set; }

        public QTestModules SuiteId { get; set; }

        public QTestProjects Project { get; set; }

        public QTestIdAttribute()
        {
        }

        public QTestIdAttribute(QTestProjects project)
        {
            Project = project;
        }

        public QTestIdAttribute(string testCaseId, QTestModules suiteId)
        {
            TestCaseId = testCaseId;
            SuiteId = suiteId;
        }

        public QTestIdAttribute(
           string testCaseId,
           QTestModules suiteId,
           QTestProjects project)
          : this(testCaseId, suiteId)
        {
            TestCaseId = testCaseId;
            SuiteId = suiteId;
            Project = project;
        }
    }
}