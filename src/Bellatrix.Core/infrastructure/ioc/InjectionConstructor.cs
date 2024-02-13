// <copyright file="InjectionConstructor.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
namespace Bellatrix;

public class InjectionConstructor
{
    public InjectionConstructor(params object[] parameters) => Parameters = parameters;

    public object[] Parameters { get; set; }
}