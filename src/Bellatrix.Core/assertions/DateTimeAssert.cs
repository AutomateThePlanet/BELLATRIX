// <copyright file="DateTimeAssert.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Assertions;

public static class DateTimeAssert
{
    public static void AreEqual(
        DateTime? expectedDate,
        DateTime? actualDate,
        DateTimeDeltaType deltaType,
        int count,
        string exceptionMessage = "")
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

        var expectedDelta = GetTimeSpanDeltaByType(deltaType, count);
        var totalSecondsDifference = Math.Abs(((DateTime)actualDate - (DateTime)expectedDate).TotalSeconds);

        if (totalSecondsDifference > expectedDelta.TotalSeconds)
        {
            throw new Exception($"{exceptionMessage}\nExpected Date: {expectedDate}, Actual Date: {actualDate} \nExpected Delta: {expectedDelta}, Actual Delta in seconds- {totalSecondsDifference} (Delta Type: {deltaType})");
        }
    }

    public static void AreEqual(
        DateTime? expectedDate, DateTime? actualDate, TimeSpan expectedDelta, string exceptionMessage = "")
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

        var difference = ((DateTime)actualDate - (DateTime)expectedDate).Duration();

        if (difference > expectedDelta)
        {
            throw new Exception($"{exceptionMessage}\nExpected Date: {expectedDate}, Actual Date: {actualDate} \nExpected Delta: {expectedDelta}, Actual Delta: {difference})");
        }
    }

    public static bool Compare(string dateTimeOne, string dateTimeTwo)
    {
        if (!DateTime.TryParse(dateTimeOne, out var one))
        {
            throw new ArgumentException("The first date provided has invalid format:{0}", dateTimeOne);
        }

        if (!DateTime.TryParse(dateTimeTwo, out var two))
        {
            throw new ArgumentException("The second date provided has invalid format:{0}", dateTimeTwo);
        }

        if (one.Ticks == two.Ticks)
        {
            return true;
        }

        return false;
    }

    private static TimeSpan GetTimeSpanDeltaByType(DateTimeDeltaType type, int count)
    {
        TimeSpan result;

        switch (type)
        {
            case DateTimeDeltaType.Days:
                result = new TimeSpan(count, 0, 0, 0);
                break;
            case DateTimeDeltaType.Minutes:
                result = new TimeSpan(0, count, 0);
                break;
            case DateTimeDeltaType.Hours:
                result = new TimeSpan(count, 0, 0);
                break;
            case DateTimeDeltaType.Seconds:
                result = new TimeSpan(0, 0, count);
                break;
            default:
                throw new NotImplementedException("The type is not implemented yet.");
        }

        return result;
    }
}