using System;
using System.Collections.Generic;

public static class Utility {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static void ApplyWhileTrue<T>(this IEnumerable<T> source, Func<int, T, bool> action)
    {
        int i = 0;
        foreach (var item in source)
            if (!action(i, item))
                break;
    }
}
