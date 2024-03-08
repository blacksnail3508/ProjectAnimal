using System.Collections.Generic;
using System.Linq;

public static class ListExtension
{
    public static void Remove<T>(this List<T> source , List<T> elementsToRemove)
    {
        source.RemoveAll(element => elementsToRemove.Contains(element));
    }
    public static void Add<T>(this List<T> list , List<T> otherList)
    {
        list.AddRange(otherList);
    }
    public static bool AnyMatch<T>(this List<T> list1 , List<T> list2)
    {
        return list1.Any(item => list2.Contains(item));
    }
}