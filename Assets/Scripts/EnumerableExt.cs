using System.Collections.Generic;
using System.Linq;

public static class EnumerableExt
{
    public static IEnumerable<int> To(this int from, int to)
    { 
        return from < to 
            ? Enumerable.Range(from, to - from + 1) 
            : Enumerable.Range(to, from - to + 1).Reverse();
    }
}