// <copyright file="NotProperlyResolvedException.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;

namespace Bellatrix
{
    public class NotProperlyResolvedException : Exception
    {
        public NotProperlyResolvedException()
        {
        }

        public NotProperlyResolvedException(string message)
        : base(message)
        {
        }

        public NotProperlyResolvedException(string message, Exception inner)
        : base(message, inner)
        {
        }

        public override string StackTrace => string.Empty;
    }
}
