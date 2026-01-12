using System.Collections.Generic;

namespace Bellatrix.Web.ComponentsJS.CKEditor;
public static class AlignmentDirectionExtensions
{
    private static readonly Dictionary<AlignmentDirection, (string Title, string ValueInDOM)> _details = new()
    {
        { AlignmentDirection.AlignLeft, ("Align left", "left") },
        { AlignmentDirection.AlignRight, ("Align right", "right") },
        { AlignmentDirection.AlignCenter, ("Align center", "center") },
        { AlignmentDirection.Justify, ("Align justify", "justify") }
    };

    public static string GetTitle(this AlignmentDirection direction)
    {
        return _details[direction].Title;
    }

    public static string GetValueInDOM(this AlignmentDirection direction)
    {
        return _details[direction].ValueInDOM;
    }
}

