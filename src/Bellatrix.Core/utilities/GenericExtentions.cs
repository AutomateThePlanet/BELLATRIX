using System.Collections.Generic;

namespace Bellatrix.Extensions;

public static class GenericExtentions
{
    public static List<T> ToEntityList<T>(this T entity)
        where T : class
    {
        return new List<T> { entity };
    }
}
