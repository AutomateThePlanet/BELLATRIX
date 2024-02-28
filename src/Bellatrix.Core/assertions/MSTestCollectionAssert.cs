// <copyright file="MSTestCollectionAssert.cs" company="Automate The Planet Ltd.">
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
using System.Collections;
using MU = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Assertions.MSTest;

public class MsTestCollectionAssert : ICollectionAssert
{
    public void AllItemsAreInstancesOfType(ICollection collection, Type expectedType) => MU.CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType);

    public void AllItemsAreInstancesOfType(ICollection collection, Type expectedType, string message) => MU.CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, message);

    public void AllItemsAreNotNull(ICollection collection) => MU.CollectionAssert.AllItemsAreNotNull(collection);

    public void AllItemsAreNotNull(ICollection collection, string message) => MU.CollectionAssert.AllItemsAreNotNull(collection, message);

    public void AllItemsAreUnique(ICollection collection) => MU.CollectionAssert.AllItemsAreUnique(collection);

    public void AllItemsAreUnique(ICollection collection, string message) => MU.CollectionAssert.AllItemsAreUnique(collection, message);

    public void AreEqual(ICollection expected, ICollection actual) => MU.CollectionAssert.AreEqual(expected, actual);

    public void AreEqual(ICollection expected, ICollection actual, IComparer comparer) => MU.CollectionAssert.AreEqual(expected, actual, comparer);

    public void AreEqual(ICollection expected, ICollection actual, string message) => MU.CollectionAssert.AreEqual(expected, actual, message);

    public void AreEqual(ICollection expected, ICollection actual, IComparer comparer, string message) => MU.CollectionAssert.AreEqual(expected, actual, comparer, message);

    public void AreEquivalent(ICollection expected, ICollection actual) => MU.CollectionAssert.AreEquivalent(expected, actual);

    public void AreEquivalent(ICollection expected, ICollection actual, string message) => MU.CollectionAssert.AreEquivalent(expected, actual, message);

    public void AreNotEqual(ICollection notExpected, ICollection actual) => MU.CollectionAssert.AreNotEqual(notExpected, actual);

    public void AreNotEqual(ICollection notExpected, ICollection actual, IComparer comparer) => MU.CollectionAssert.AreNotEqual(notExpected, actual, comparer);

    public void AreNotEqual(ICollection notExpected, ICollection actual, string message) => MU.CollectionAssert.AreNotEqual(notExpected, actual, message);

    public void AreNotEqual(ICollection notExpected, ICollection actual, IComparer comparer, string message) => MU.CollectionAssert.AreNotEqual(notExpected, actual, comparer, message);

    public void AreNotEquivalent(ICollection expected, ICollection actual) => MU.CollectionAssert.AreNotEquivalent(expected, actual);

    public void AreNotEquivalent(ICollection expected, ICollection actual, string message) => MU.CollectionAssert.AreNotEquivalent(expected, actual, message);

    public void Contains(ICollection collection, object element) => MU.CollectionAssert.Contains(collection, element);

    public void Contains(ICollection collection, object element, string message) => MU.CollectionAssert.Contains(collection, element, message);

    public void DoesNotContain(ICollection collection, object element) => MU.CollectionAssert.DoesNotContain(collection, element);

    public void DoesNotContain(ICollection collection, object element, string message) => MU.CollectionAssert.DoesNotContain(collection, element, message);

    public void IsNotSubsetOf(ICollection subset, ICollection superset) => MU.CollectionAssert.IsNotSubsetOf(subset, superset);

    public void IsNotSubsetOf(ICollection subset, ICollection superset, string message) => MU.CollectionAssert.IsNotSubsetOf(subset, superset, message);

    public void IsSubsetOf(ICollection subset, ICollection superset) => MU.CollectionAssert.IsSubsetOf(subset, superset);

    public void IsSubsetOf(ICollection subset, ICollection superset, string message) => MU.CollectionAssert.IsSubsetOf(subset, superset, message);
}