using System.Collections.Generic;
using System.Linq;

public static class LinqExtensions
{
    public static bool HasValue<T>(this List<T> list)
    {
        return list != null && list.Any();
    }
}
