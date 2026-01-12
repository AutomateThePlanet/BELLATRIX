using System.Collections.Generic;

namespace Bellatrix.Web.ComponentsJS.CKEditor;

public static class HeadingOptionsEnumExtensions
{
    private static readonly Dictionary<HeadingOptionsEnum, (string Value, string OpenTag, string ClosingTag)> _details = new()
    {
        { HeadingOptionsEnum.Heading1, ("Heading 1", "<h1>", "</h1>") },
        { HeadingOptionsEnum.Heading2, ("Heading 2", "<h2>", "</h2>") },
        { HeadingOptionsEnum.Heading3, ("Heading 3", "<h3>", "</h3>") },
        { HeadingOptionsEnum.Heading4, ("Heading 4", "<h4>", "</h4>") }
    };

    public static string GetValue(this HeadingOptionsEnum option)
    {
        return _details[option].Value;
    }

    public static string GetOpenTag(this HeadingOptionsEnum option)
    {
        return _details[option].OpenTag;
    }

    public static string GetClosingTag(this HeadingOptionsEnum option)
    {
        return _details[option].ClosingTag;
    }
}

