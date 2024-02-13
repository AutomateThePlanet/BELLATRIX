// <copyright file="ICollectionAssert.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Assertions;

public interface ICollectionAssert
{
    void AllItemsAreInstancesOfType(ICollection collection, Type expectedType);

    void AllItemsAreInstancesOfType(ICollection collection, Type expectedType, string message);

    void AllItemsAreNotNull(ICollection collection);

    void AllItemsAreNotNull(ICollection collection, string message);

    void AllItemsAreUnique(ICollection collection);

    void AllItemsAreUnique(ICollection collection, string message);

    void AreEqual(ICollection expected, ICollection actual);

    void AreEqual(ICollection expected, ICollection actual, IComparer comparer);

    void AreEqual(ICollection expected, ICollection actual, string message);

    void AreEqual(ICollection expected, ICollection actual, IComparer comparer, string message);

    void AreEquivalent(ICollection expected, ICollection actual);

    void AreEquivalent(ICollection expected, ICollection actual, string message);

    void AreNotEqual(ICollection notExpected, ICollection actual);

    void AreNotEqual(ICollection notExpected, ICollection actual, IComparer comparer);

    void AreNotEqual(ICollection notExpected, ICollection actual, string message);

    void AreNotEqual(ICollection notExpected, ICollection actual, IComparer comparer, string message);

    void AreNotEquivalent(ICollection expected, ICollection actual);

    void AreNotEquivalent(ICollection expected, ICollection actual, string message);

    void Contains(ICollection collection, object element);

    void Contains(ICollection collection, object element, string message);

    void DoesNotContain(ICollection collection, object element);

    void DoesNotContain(ICollection collection, object element, string message);

    void IsNotSubsetOf(ICollection subset, ICollection superset);

    void IsNotSubsetOf(ICollection subset, ICollection superset, string message);

    void IsSubsetOf(ICollection subset, ICollection superset);

    void IsSubsetOf(ICollection subset, ICollection superset, string message);
}