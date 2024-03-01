// <copyright file="OverrideParameter.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
namespace Bellatrix;

public class OverrideParameter
{
    public OverrideParameter(string parameterName, object parameterValue)
    {
        ParameterName = parameterName;
        ParameterValue = parameterValue;
    }

    public string ParameterName { get; set; }

    public object ParameterValue { get; set; }
}