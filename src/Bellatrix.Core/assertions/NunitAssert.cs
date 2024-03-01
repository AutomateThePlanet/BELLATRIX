// <copyright file="NunitAssert.cs" company="Automate The Planet Ltd.">
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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NU = NUnit.Framework;

namespace Bellatrix.Assertions.NUnit;

public class NUnitAssert : IAssert
{
    public void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds) => NU.Assert.That(actualDate, NU.Is.EqualTo(expectedDate).Within(deltaSeconds).Seconds);

    public void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds, string message) => NU.Assert.That(actualDate, NU.Is.EqualTo(expectedDate).Within(deltaSeconds).Seconds, message);

    public void AreEqual(double expected, double actual, double delta) => NU.Assert.That(actual, Is.EqualTo(expected).Within(delta));

    public void AreEqual(double expected, double actual, double delta, string message) => NU.Assert.That(actual, Is.EqualTo(expected).Within(delta), message);

    public void AreEqual(object expected, object actual) => NU.Assert.That(actual, NU.Is.EqualTo(expected));

    public void AreEqual(object expected, object actual, string message) => NU.Assert.That(actual, NU.Is.EqualTo(expected), message);

    public void AreEqual<T>(T expected, T actual) => NU.Assert.That(actual, NU.Is.EqualTo(expected));

    public void AreEqual<T>(T expected, T actual, string message) => NU.Assert.That(actual, NU.Is.EqualTo(expected), message);

    public void AreNotEqual(object expected, object actual) => NU.Assert.That(actual, NU.Is.Not.EqualTo(expected));

    public void AreNotEqual(object expected, object actual, string message) => NU.Assert.That(actual, NU.Is.Not.EqualTo(expected), message);

    public void AreNotEqual<T>(T expected, T actual) => NU.Assert.That(actual, NU.Is.Not.EqualTo(expected));

    public void AreNotEqual<T>(T expected, T actual, string message) => NU.Assert.That(actual, NU.Is.Not.EqualTo(expected), message);

    public void Fail(string message) => NU.Assert.Fail(message);

    public void Fail(string message, params object[] parameters)
    {
        throw new NotImplementedException();
    }

    public void IsFalse(bool condition) => NU.Assert.That(condition, NU.Is.False);

    public void IsFalse(bool condition, string message) => NU.Assert.That(condition, NU.Is.False, message);

    public void IsInstanceOfType(object value, Type expectedType) => NU.Assert.That(value, NU.Is.AssignableTo(expectedType));

    public void IsInstanceOfType(object value, Type expectedType, string message) => NU.Assert.That(value, NU.Is.AssignableTo(expectedType), message);

    public void IsNotNull(object value) => NU.Assert.That(value, NU.Is.Not.Null);

    public void IsNotNull(object value, string message) => NU.Assert.That(value, NU.Is.Not.Null, message);

    public void IsNull(object value) => NU.Assert.That(value, NU.Is.Null);

    public void IsNull(object value, string message) => NU.Assert.That(value, NU.Is.Null, message);

    public void IsTrue(bool condition) => NU.Assert.That(condition, NU.Is.True);

    public void IsTrue(bool condition, string message) => NU.Assert.That(condition, NU.Is.True, message);

    public void IsTrue(bool condition, string message, params object[] parameters) => NU.Assert.That(condition, NU.Is.True, string.Format(message, parameters));

    public void Multiple(params Action[] assertions) => Assert.Multiple(assertions);
}