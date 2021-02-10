// <copyright file="EntitiesAsserter.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
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

namespace Bellatrix.Assertions
{
    public static class EntitiesAsserter
    {
        public static void AreEqual<TEntity>(TEntity expectedObject, TEntity realObject, params string[] propertiesNotToCompare)
            where TEntity : new()
        {
            List<Exception> failedAssertions = new List<Exception>();

            var properties = realObject.GetType().GetProperties();
            foreach (var currentRealProperty in properties)
            {
                if (!propertiesNotToCompare.Contains(currentRealProperty.Name))
                {
                    var currentExpectedProperty = expectedObject.GetType().GetProperty(currentRealProperty.Name);
                    var exceptionMessage = $"The property {currentRealProperty.Name} of class {currentRealProperty.DeclaringType.Name} was not as expected.";

                    try
                    {
                        if (currentRealProperty.PropertyType == typeof(DateTime) ||
                        currentRealProperty.PropertyType == typeof(DateTime?) ||
                        currentRealProperty.PropertyType == typeof(DateTimeOffset))
                        {
                            Assert.AreDateTimesEqual(
                                currentExpectedProperty?.GetValue(expectedObject, null) as DateTime?,
                                currentRealProperty.GetValue(realObject, null) as DateTime?,
                                300,
                                exceptionMessage);
                        }
                        else if (currentExpectedProperty?.GetValue(expectedObject, null) == string.Empty)
                        {
                            Assert.IsTrue(string.IsNullOrEmpty((string)currentRealProperty.GetValue(realObject, null)), exceptionMessage);
                        }
                        else
                        {
                            Assert.AreEqual(
                                currentExpectedProperty?.GetValue(expectedObject, null),
                                currentRealProperty.GetValue(realObject, null),
                                exceptionMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        failedAssertions.Add(ex);
                    }
                }
            }

            if (failedAssertions.Count > 1)
            {
                // All errors have been added already, so we need only to fail the assert
                Assert.Fail(string.Empty);
            }
        }
    }
}