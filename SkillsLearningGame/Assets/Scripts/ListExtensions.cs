using System.Collections;
using System.Collections.Generic;
using System;

// Extension functions for lists in our project
public static class ListExtensions
{

    //A simple shuffle action that can easily shuffle a list, to help with card, action, enemy and question shuffling
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rnd = new Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }

    // A swap function used by the shuffling algorithm
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
