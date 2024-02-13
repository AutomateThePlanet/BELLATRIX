// <copyright file="Assert.cs" company="Automate The Planet Ltd.">
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
using System.Text;

namespace Bellatrix.Assertions;

public sealed class Assert
{
    private static readonly IAssert _assert;

    static Assert()
    {
        _assert = ServicesCollection.Current.Resolve<IAssert>();

        if (_assert == null)
        {
            throw new NullReferenceException($"No unit test framework registered. Register one in the TestInitilize class");
        }
    }

    public static void AreEqual(double expected, double actual, double delta) => _assert.AreEqual(expected, actual, delta);

    public static void AreEqual(double expected, double actual, double delta, string message) => _assert.AreEqual(expected, actual, delta, message);

    public static void AreEqual(object expected, object actual) => _assert.AreEqual(expected, actual);

    public static void AreEqual(object expected, object actual, string message) => _assert.AreEqual(expected, actual, message);

    public static void AreEqual<T>(T expected, T actual) => _assert.AreEqual(expected, actual);

    public static void AreEqual<T>(T expected, T actual, string message) => _assert.AreEqual(expected, actual, message);

    public static void AreEqual<TException, T>(T expected, T actual, string message)
    where TException : Exception, new()
    {
        try
        {
            _assert.AreEqual(expected, actual, message);
        }
        catch (Exception)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }

    public static void AreNotEqual(object expected, object actual) => _assert.AreNotEqual(expected, actual);

    public static void AreNotEqual(object expected, object actual, string message) => _assert.AreNotEqual(expected, actual, message);

    public static void AreNotEqual<T>(T expected, T actual) => _assert.AreNotEqual(expected, actual);

    public static void AreNotEqual<T>(T expected, T actual, string message) => _assert.AreNotEqual(expected, actual, message);

    public static void IsFalse(bool condition) => _assert.IsFalse(condition);

    public static void IsFalse(bool condition, string message) => _assert.IsFalse(condition, message);

    public static void IsTrue(bool condition) => _assert.IsTrue(condition);

    public static void IsTrue(bool condition, string message, params object[] parameters) => _assert.IsTrue(condition, message, parameters);

    public static void IsTrue(bool condition, string message) => _assert.IsTrue(condition, message);

    public static void IsTrue<TException>(bool condition, string message)
    where TException : Exception, new()
    {
        try
        {
            _assert.IsTrue(condition, message);
        }
        catch (Exception)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }

    public static void Fail(string message, params object[] parameters) => _assert.Fail(message, parameters);

    public static void IsNull(object value) => _assert.IsNull(value);

    public static void IsNull(object value, string message) => _assert.IsNull(value, message);

    public static void IsNotNull(object value) => _assert.IsNotNull(value);

    public static void IsNotNull(object value, string message) => _assert.IsNotNull(value, message);

    public static void Fail(string message) => _assert.Fail(message);

    public static void IsInstanceOfType(object value, Type expectedType) => _assert.IsInstanceOfType(value, expectedType);

    public static void IsInstanceOfType(object value, Type expectedType, string message) => _assert.IsInstanceOfType(value, expectedType, message);

    public static void AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds) => _assert.AreDateTimesEqual(expectedDate, actualDate, deltaSeconds);

    public static void AreDateTimesEqual(
        DateTime? expectedDate, DateTime? actualDate, int deltaSeconds, string message) => _assert.AreDateTimesEqual(expectedDate, actualDate, deltaSeconds, message);

    public static void Multiple(params Action[] assertions)
    {
        var assertionExceptions = new List<Exception>();
        foreach (var assertion in assertions)
        {
            try
            {
                assertion();
            }
            catch (Exception e)
            {
                assertionExceptions.Add(e);
            }
        }

        if (assertionExceptions.Any())
        {
            throw new MultipleAssertException(assertionExceptions);
        }
    }
}