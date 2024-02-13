// <copyright file="DynamicTestCasesAssertExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.DynamicTestCases;

namespace Bellatrix.Api;

public class DynamicTestCasesAssertExtensions : AssertExtensionsEventHandlers
{
    protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

    protected override void AssertContentContainsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"contains {arg.ActionValue}.");

    protected override void AssertContentEncodingEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response Cache-Info header ", $"is equal to {arg.ActionValue}.");

    protected override void AssertContentEqualsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"is equal to {arg.ActionValue}.");

    protected override void AssertContentNotContainsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"does not contain {arg.ActionValue}.");

    protected override void AssertContentNotEqualsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"is not equal to {arg.ActionValue}.");

    protected override void AssertContentTypeEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response Content-Type ", $"is equal to {arg.ActionValue}.");

    protected override void AssertCookieEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response cookie ", $"is equal to {arg.ActionValue}.");

    protected override void AssertCookieExistsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response ", $"cookie {arg.ActionValue} exists.");

    protected override void AssertExecutionTimeUnderEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response execution time ", $"is under {arg.ActionValue}.");

    protected override void AssertResponseHeaderEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response header ", $"is equal to {arg.ActionValue}.");

    protected override void AssertResultEqualsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"is equal to {arg.ActionValue}.");

    protected override void AssertResultNotEqualsEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response content ", $"is not equal to {arg.ActionValue}.");

    protected override void AssertStatusCodeEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response status code ", $"is equal to {arg.ActionValue}.");

    protected override void AssertSuccessStatusCodeEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response status code ", "is successfull.");

    protected override void AssertSchemaEventHandler(object sender, ApiAssertEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert response ", "is compatible to specified schema.");
}