// <copyright file="MSTestAssert.cs" company="Automate The Planet Ltd.">
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
using MU = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Assertions.MSTest;

public class MsTestAssert : IAssert
{
    public void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds) => AreDateTimesEqual(expectedDate, actualDate, deltaSeconds, string.Empty);

    public void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds, string message)
    {
        if (expectedDate == null &&
            actualDate == null)
        {
            return;
        }

        if (expectedDate == null)
        {
            throw new NullReferenceException("The expected date was null");
        }

        if (actualDate == null)
        {
            throw new NullReferenceException("The actual date was null");
        }

        var expectedDelta = new TimeSpan(0, 0, deltaSeconds);
        var totalSecondsDifference = Math.Abs(((DateTime)actualDate - (DateTime)expectedDate).TotalSeconds);

        if (totalSecondsDifference > expectedDelta.TotalSeconds)
        {
            MU.Assert.Fail($"The expected datetime is different than the actual one. Expected was: {expectedDate}, actual was: {actualDate}. The delta was {deltaSeconds} seconds. {message}");
        }
    }

    public void AreEqual(double expected, double actual, double delta) => MU.Assert.AreEqual(expected, actual, delta);

    public void AreEqual(double expected, double actual, double delta, string message) => MU.Assert.AreEqual(expected, actual, delta, message);

    public void AreEqual(object expected, object actual) => MU.Assert.AreEqual(expected, actual);

    public void AreEqual(object expected, object actual, string message) => MU.Assert.AreEqual(expected, actual, message);

    public void AreEqual<T>(T expected, T actual) => MU.Assert.AreEqual(expected, actual);

    public void AreEqual<T>(T expected, T actual, string message) => MU.Assert.AreEqual(expected, actual, message);

    public void AreNotEqual(object expected, object actual) => MU.Assert.AreNotEqual(expected, actual);

    public void AreNotEqual(object expected, object actual, string message) => MU.Assert.AreNotEqual(expected, actual, message);

    public void AreNotEqual<T>(T expected, T actual) => MU.Assert.AreNotEqual(expected, actual);

    public void AreNotEqual<T>(T expected, T actual, string message) => MU.Assert.AreNotEqual(expected, actual, message);

    public void Fail(string message) => MU.Assert.Fail(message);

    public void Fail(string message, params object[] parameters) => MU.Assert.Fail(message, parameters);

    public void IsFalse(bool condition) => MU.Assert.IsFalse(condition);

    public void IsFalse(bool condition, string message) => MU.Assert.IsFalse(condition, message);

    public void IsInstanceOfType(object value, Type expectedType) => MU.Assert.IsInstanceOfType(value, expectedType);

    public void IsInstanceOfType(object value, Type expectedType, string message) => MU.Assert.IsInstanceOfType(value, expectedType, message);

    public void IsNotNull(object value) => MU.Assert.IsNotNull(value);

    public void IsNotNull(object value, string message) => MU.Assert.IsNotNull(value, message);

    public void IsNull(object value) => MU.Assert.IsNull(value);

    public void IsNull(object value, string message) => MU.Assert.IsNull(value, message);

    public void IsTrue(bool condition) => MU.Assert.IsTrue(condition);

    public void IsTrue(bool condition, string message) => MU.Assert.IsTrue(condition, message);

    public void IsTrue(bool condition, string message, params object[] parameters) => MU.Assert.IsTrue(condition, message, parameters);

    public void Multiple(params Action[] assertions) => Assert.Multiple(assertions);
}