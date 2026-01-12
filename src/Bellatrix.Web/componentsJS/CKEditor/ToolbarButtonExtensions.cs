namespace Bellatrix.Web.ComponentsJS.CKEditor;
using System.Collections.Generic;

public static class ToolbarButtonExtensions
{
    private static readonly Dictionary<ToolbarButton, string> _values = new()
    {
        { ToolbarButton.Bold, "bold" },
        { ToolbarButton.Italic, "italic" },
        { ToolbarButton.Underline, "underline" },
        { ToolbarButton.Link, "link" },
        { ToolbarButton.UnorderedList, "unordered-list" },
        { ToolbarButton.OrderedList, "ordered-list" },
        { ToolbarButton.InsertTable, "table" }
    };

    public static string GetValue(this ToolbarButton button)
    {
        return _values[button];
    }
}

