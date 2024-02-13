// <copyright file="BugReportingContextService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bellatrix.BugReporting;

public class BugReportingContextService
{
    public BugReportingContextService()
    {
        Context = new ThreadLocal<BugReportingContext>(() => new BugReportingContext());
    }

    public ThreadLocal<BugReportingContext> Context { get; internal set; }

    public void ResetContext()
    {
        Context = new ThreadLocal<BugReportingContext>(() => new BugReportingContext());
    }

    // Appends precondition to the test case.
    public void AddPrecondition(string precondition)
    {
        Context.Value.Precondition += precondition;
    }

    // Adds a regular test step
    public void AddStep(string description)
    {
        Context.Value.TestSteps.Add(new TestStep(description));
    }

    // Adds a test step, using description generated from the method name, from which it was invoked.
    public void AddMethodAsStep(params string[] parameters)
    {
        var stackTrace = new StackTrace();
        var methodInfo = stackTrace.GetFrame(1).GetMethod() as MethodInfo;
        var description = MethodToSentence(methodInfo.Name);
        if (parameters.Any())
        {
            description += " using data: " + parameters.Stringify();
        }

        Context.Value.TestSteps.Add(new TestStep(description));
    }

    // Adds test case using the description and expected result columns.
    public void AddAssertStep(string assertDescription, string expectedResult)
    {
        Context.Value.TestSteps.Add(new TestStep(assertDescription, expectedResult));
    }

    public void AddAdditionalProperty(string name, string value)
    {
        if (Context.Value.AdditionalProperties.ContainsKey(name))
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Context.Value.AdditionalProperties[name] = value;
        }
        else
        {
            Context.Value.AdditionalProperties.Add(name, value);
        }
    }

    // Converts a method name to a human-readable sentence
    private string MethodToSentence(string name)
    {
        var returnStr = name;
        for (var i = 1; i < name.Length; i++)
        {
            var letter = name.Substring(i, 1);

            if (letter.GetHashCode() != letter.ToLower().GetHashCode())
            {
                returnStr = returnStr.Replace(letter, $" {letter.ToLower()}");
            }
        }

        returnStr = RemoveSpecialCharacters(returnStr);
        returnStr = returnStr.First().ToString().ToUpper() + returnStr.Substring(1);

        return returnStr;
    }

    private string RemoveSpecialCharacters(string nameValue) => new Regex(@"[^a-zA-Z0-9]|[0]").Replace(nameValue, string.Empty);
}
