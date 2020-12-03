// <copyright file="NotResolvedExceptionAttribute.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;

namespace Bellatrix
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotResolvedExceptionAttribute : Attribute
    {
        public NotResolvedExceptionAttribute(string exceptionMessage) => ExceptionMessage = exceptionMessage;

        public string ExceptionMessage { get; set; }
    }
}