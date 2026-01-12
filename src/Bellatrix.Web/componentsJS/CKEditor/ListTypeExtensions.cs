namespace Bellatrix.Web.ComponentsJS.CKEditor;
using System.Collections.Generic;

public static class ListTypeExtensions
{
    private static readonly Dictionary<ListType, (string OpenTag, string ClosingTag)> _details = new()
    {
        { ListType.NumberedList, ("<ol>", "</ol>") },
        { ListType.BulletedList, ("<ul>", "</ul>") }
    };

    public static string GetOpenTag(this ListType listType)
    {
        return _details[listType].OpenTag;
    }

    public static string GetClosingTag(this ListType listType)
    {
        return _details[listType].ClosingTag;
    }
}

