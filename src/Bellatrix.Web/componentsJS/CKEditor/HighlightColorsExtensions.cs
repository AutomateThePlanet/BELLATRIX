namespace Bellatrix.Web.ComponentsJS.CKEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public static class HighlightColorsExtensions
{
    private static readonly Dictionary<HighlightColors, (string Title, string ClassValueInDOM, string RgbValue)> _details = new()
    {
        { HighlightColors.OrangeMarker, ("Orange marker", "marker-orange", "254, 133, 11") },
        { HighlightColors.GreenMarker, ("Green marker", "marker-green", "98, 249, 98") },
        { HighlightColors.RedPen, ("Red pen", "pen-red", "231, 19, 19") },
        { HighlightColors.GreenPen, ("Green pen", "pen-green", "18, 138, 0") }
    };

    public static string GetTitle(this HighlightColors color)
    {
        return _details[color].Title;
    }

    public static string GetClassValueInDOM(this HighlightColors color)
    {
        return _details[color].ClassValueInDOM;
    }

    public static string GetRgbValue(this HighlightColors color)
    {
        return _details[color].RgbValue;
    }

    public static HighlightColors GetRandomHighlightColor()
    {
        var random = new Random();
        var values = Enum.GetValues(typeof(HighlightColors)).Cast<HighlightColors>().ToList();
        return values[random.Next(values.Count)];
    }
}

